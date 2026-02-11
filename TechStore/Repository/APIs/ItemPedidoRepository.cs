using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Repository.api
{
    public class ItemPedidoRepository
    {
        private readonly TechStoreContext _context;

        public ItemPedidoRepository(TechStoreContext context)
        {
            _context = context;
        }

        public async Task<ItemPedido?> BuscarItemPorPedidoEProduto(
            int pedidoId,
            int produtoId)
        {
            return await _context.ItensPedido
                .FirstOrDefaultAsync(item =>
                    item.PedidoId == pedidoId &&
                    item.ProdutoId == produtoId);
        }

        public async Task<ItemPedido?> BuscarItemPorId(int itemId)
        {
            return await _context.ItensPedido.FindAsync(itemId);
        }

        public async Task AdicionarItem(ItemPedido item)
        {
            _context.ItensPedido.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarItem(ItemPedido item)
        {
            _context.ItensPedido.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarItem(int id)
        {
            await _context.ItensPedido.Where(item => item.Id == id).ExecuteDeleteAsync();
        }
    }
}
