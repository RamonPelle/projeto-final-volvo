using TechStore.Models;
using TechStore.Models.DTOs.Request;
using TechStore.Repository.api;

namespace TechStore.Services.api
{
    public class ItemPedidoService
    {
        private readonly ItemPedidoRepository _itemPedidoRepository;
        private readonly PedidoRepository _pedidoRepository;
        private readonly ProdutoRepository _produtoRepository;

        public ItemPedidoService(
            ItemPedidoRepository itemPedidoRepository,
            PedidoRepository pedidoRepository,
            ProdutoRepository produtoRepository)
        {
            _itemPedidoRepository = itemPedidoRepository;
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task AdicionarItem(int pedidoId, ItemPedidoRequest itemPedidoRequest)
        {
            if (pedidoId <= 0)
                throw new ArgumentException("Id de pedido não pode ser negativa.");

            if (itemPedidoRequest.Quantidade <= 0)
                throw new ArgumentException("Quantidade não pode ser negativa.");

            if (itemPedidoRequest.ProdutoId <= 0)
                throw new ArgumentException("Id de produto não pode ser negativa.");

            var pedido = await _pedidoRepository.BuscarPedidoPorId(pedidoId)
                ?? throw new ArgumentException($"Pedido com id {pedidoId} não encontrado.");

            var produto = await _produtoRepository.BuscarProdutoPorId(itemPedidoRequest.ProdutoId)
                ?? throw new ArgumentException($"Produto com id {itemPedidoRequest.ProdutoId} não encontrado.");

            if (produto.Estoque <= 0)
                throw new InvalidOperationException("Produto sem estoque disponível.");

            var itemExistente =
                await _itemPedidoRepository.BuscarItemPorPedidoEProduto(pedidoId, produto.Id);

            if (itemExistente != null)
            {
                itemExistente.Quantidade += 1;
                await _itemPedidoRepository.AtualizarItem(itemExistente);
                return;
            }

            var novoItem = new ItemPedido
            {
                PedidoId = pedido.Id,
                ProdutoId = produto.Id,
                Quantidade = 1,
                PrecoUnitario = produto.Preco
            };

            await _itemPedidoRepository.AdicionarItem(novoItem);
        }

        public async Task DeletarItem(int pedidoId, int itemId)
        {
            if (pedidoId <= 0)
                throw new ArgumentException("PedidoId deve ser maior que zero.", nameof(pedidoId));

            if (itemId <= 0)
                throw new ArgumentException("ItemId deve ser maior que zero.", nameof(itemId));

            var item = await _itemPedidoRepository.BuscarItemPorId(itemId)
                ?? throw new KeyNotFoundException($"Item com id {itemId} não encontrado.");

            if (item.PedidoId != pedidoId)
                throw new InvalidOperationException(
                    "Item não encontrado no pedido."
                );

            await _itemPedidoRepository.DeletarItem(itemId);
        }

    }
}
