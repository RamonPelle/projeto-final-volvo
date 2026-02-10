using TechStore.Models;
using TechStore.Repository.api;
using TechStore.Models.DTOs.Request;
using TechStore.Models.Enums;
using TechStore.Models.DTOs.Response;

namespace TechStore.Services.api
{
    public class PedidoService
    {
        private readonly PedidoRepository _pedidoRepository;
        private readonly ItemPedidoRepository _itemPedidoRepository;
        private readonly ProdutoRepository _produtoRepository;

        public PedidoService(
            PedidoRepository pedidoRepository,
            ItemPedidoRepository itemPedidoRepository,
            ProdutoRepository produtoRepository
        )
        {
            _pedidoRepository = pedidoRepository
                ?? throw new ArgumentNullException(nameof(pedidoRepository));

            _itemPedidoRepository = itemPedidoRepository
                ?? throw new ArgumentNullException(nameof(itemPedidoRepository));

            _produtoRepository = produtoRepository
                ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        public async Task<List<Pedido>> BuscarTodosOsPedidos(int? clienteId, StatusPedido? status)
        {
            return await _pedidoRepository.BuscarTodosOsPedidos(clienteId, status);
        }

        public async Task<Pedido?> BuscarPedidoPorId(int id)
        {
            return await _pedidoRepository.BuscarPedidoPorId(id);
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

            var novoPedido = new Pedido
            {
                ClienteId = pedidoRequest.ClienteId,
                Data = DateTime.UtcNow,
                Status = StatusPedido.Pendente
            };

            await _pedidoRepository.CriarPedido(novoPedido);

            if (pedidoRequest.Itens != null && pedidoRequest.Itens.Any())
                await AdicionarItens(novoPedido, pedidoRequest.Itens);

            return novoPedido;
        }

        public async Task<IEnumerable<ValorPorCategoriaResponse>> ObterValorTotalVendidoPorCategoria()
        {
            return await _pedidoRepository.ObterValorTotalVendidoPorCategoria();
        }

        private async Task AdicionarItens(
            Pedido pedido,
            List<ItemPedidoRequest> itens
        )
        {
            foreach (var itemPedidoRequest in itens)
            {
                var produto =
                    await _produtoRepository.BuscarProdutoPorId(itemPedidoRequest.ProdutoId)
                    ?? throw new ArgumentException(
                        $"Produto {itemPedidoRequest.ProdutoId} não encontrado."
                    );

                var itemExistente =
                    await _itemPedidoRepository.BuscarItem(
                        pedido.Id,
                        itemPedidoRequest.ProdutoId
                    );

                if (itemExistente != null)
                {
                    itemExistente.Quantidade += itemPedidoRequest.Quantidade;
                    await _itemPedidoRepository.AtualizarItem();
                }
                else
                {
                    var novoItem = new ItemPedido
                    {
                        PedidoId = pedido.Id,
                        ProdutoId = produto.Id,
                        Quantidade = itemPedidoRequest.Quantidade,
                        PrecoUnitario = produto.Preco
                    };

                    await _itemPedidoRepository.AdicionarItem(novoItem);
                }
            }
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

        public async Task DeletarItem(int pedidoId, int itemId)
        {
            var item = await _itemPedidoRepository.BuscarItemPorId(itemId);

            if (item == null)
            {
                return;
            }
            if (item.PedidoId == pedidoId)
            {
                await _pedidoRepository.DeletarItem(itemId);
            }
            else
            {
                throw new ArgumentException("Item não pertence ao pedido.");
            }
        }

        public async Task EditarPedido(int id, PedidoEditarRequest PedidoEditarDto)
        {
            if (id <= 0)
                throw new ArgumentException("Id do pedido inválido.");

            if (PedidoEditarDto == null)
                throw new ArgumentNullException(nameof(PedidoEditarDto));

            var pedido = await _pedidoRepository.BuscarPedidoPorId(id);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            if (pedido.Status == StatusPedido.Concluido)
                throw new ArgumentException("Pedidos concluídos não podem ser alterados.");

            pedido.Status = PedidoEditarDto.Status;

            await _pedidoRepository.EditarPedido(pedido);
        }
    }
}
