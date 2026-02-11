using TechStore.Models;
using TechStore.Repository.api;
using TechStore.Models.DTOs.Request;
using TechStore.Models.Enums;
using TechStore.Models.DTOs.Response;
using AutoMapper;
using System.Transactions;

namespace TechStore.Services.api
{
    public class PedidoService
    {
        private readonly PedidoRepository _pedidoRepository;
        private readonly ItemPedidoRepository _itemPedidoRepository;
        private readonly ProdutoRepository _produtoRepository;
        private readonly ClienteRepository _clienteRepository;
        private readonly ItemPedidoService _itemPedidoService;
        private readonly ProdutoService _produtoService;

        private readonly IMapper _mapper;

        public PedidoService(
            PedidoRepository pedidoRepository,
            ItemPedidoRepository itemPedidoRepository,
            ProdutoRepository produtoRepository,
            ItemPedidoService itemPedidoService,
            ClienteRepository clienteRepository,
            ProdutoService produtoService,
            IMapper mapper
        )
        {
            _pedidoRepository = pedidoRepository
                ?? throw new ArgumentNullException(nameof(pedidoRepository));

            _itemPedidoRepository = itemPedidoRepository
                ?? throw new ArgumentNullException(nameof(itemPedidoRepository));

            _produtoRepository = produtoRepository
                ?? throw new ArgumentNullException(nameof(produtoRepository));

            _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));

            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));

            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));

            _itemPedidoService = itemPedidoService
                ?? throw new ArgumentNullException(nameof(itemPedidoService));
        }

        public async Task<List<Pedido>> BuscarTodosOsPedidos(int? clienteId, StatusPedido? status)
        {
            if (clienteId.HasValue)
            {
                var cliente = await _clienteRepository.BuscarClientePorId(clienteId.Value);
                if (cliente == null)
                    throw new KeyNotFoundException("Cliente não encontrado.");
            }

            return await _pedidoRepository.BuscarTodosOsPedidos(clienteId, status);
        }

        public async Task<Pedido?> BuscarPedidoPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id do pedido inválido.");
            var pedido = await _pedidoRepository.BuscarPedidoPorId(id);
            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            return pedido;
        }

        public async Task<Pedido> CriarPedido(PedidoRequest pedidoRequest)
        {
            var pedidoExistente = await _pedidoRepository.ObterPedidoAtivoPorCliente(pedidoRequest.ClienteId);

            if (pedidoExistente != null)
            {
                throw new ArgumentException(
                "Cliente já possui um pedido pendente."
                );
            }

            if (pedidoRequest.Itens == null || !pedidoRequest.Itens.Any())
            { throw new ArgumentException("O pedido deve conter pelo menos um item."); }

            if (pedidoRequest.ClienteId <= 0)
                throw new ArgumentException("Id do cliente inválido.");

            var cliente = await _clienteRepository.BuscarClientePorId(pedidoRequest.ClienteId);

            if (cliente == null)
                throw new KeyNotFoundException("Cliente não encontrado.");

            var novoPedido = _mapper.Map<Pedido>(pedidoRequest);
            novoPedido.Data = DateTime.UtcNow;
            novoPedido.Status = StatusPedido.Pendente;
            novoPedido.Itens = new List<ItemPedido>();

            foreach (var item in pedidoRequest.Itens)
            {
                var produto = await _produtoRepository.BuscarProdutoPorId(item.ProdutoId);

                if (produto == null)
                    throw new KeyNotFoundException($"Produto com id {item.ProdutoId} não encontrado.");

                if (item.Quantidade <= 0)
                    throw new ArgumentException("A quantidade deve ser maior que zero.");

                if (item.Quantidade > produto.Estoque)
                    throw new ArgumentException($"Quantidade solicitada para o produto {produto.Nome} excede o estoque disponível.");

                novoPedido.Itens.Add(new ItemPedido
                {
                    ProdutoId = produto.Id,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = produto.Preco
                });

            }

            await _pedidoRepository.CriarPedido(novoPedido);

            return novoPedido;
        }

        public async Task<IEnumerable<ValorPorCategoriaResponse>> ObterValorTotalVendidoPorCategoria()
        {
            return await _pedidoRepository.ObterValorTotalVendidoPorCategoria();
        }

        public async Task DeletarPedido(int id)
        {
            var pedido = await _pedidoRepository.BuscarPedidoPorId(id);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            if (pedido.Status == StatusPedido.Concluido)
                throw new ArgumentException("Pedidos concluídos não podem ser excluídos.");

            await _pedidoRepository.DeletarPedido(id);
        }

        public async Task FinalizarPedido(int id, PedidoFinalizarRequest pedidoFinalizarRequest)
        {
            if (id <= 0)
                throw new ArgumentException("Id do pedido inválido.");

            if (pedidoFinalizarRequest == null)
                throw new ArgumentNullException(nameof(pedidoFinalizarRequest));

            using (var transacaoBD = await _pedidoRepository.IniciarTransacao())
            {
                var pedido = await _pedidoRepository.BuscarPedidoPorId(id);

                if (pedido == null)
                    throw new KeyNotFoundException("Pedido não encontrado.");

                if (pedido.Status == StatusPedido.Concluido)
                    throw new ArgumentException("Pedidos concluídos não podem ser alterados.");

                pedido.Status = pedidoFinalizarRequest.Status;

                foreach (var item in pedido.Itens)
                {
                    await _produtoService.AtualizarEstoqueProdutos(item.ProdutoId, item.Quantidade * -1);
                }

                await _pedidoRepository.FinalizarPedido(pedido);

                await transacaoBD.CommitAsync();
            }
        }
    }
}
