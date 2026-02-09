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

        public async Task<ItemPedido?> BuscarItem(int pedidoId, int produtoId)
        {
            return await _context.ItensPedido
                .FirstOrDefaultAsync(i =>
                    i.PedidoId == pedidoId &&
                    i.ProdutoId == produtoId);
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

        public async Task AtualizarItem()
        {
            await _context.SaveChangesAsync();
        }
    }
}
