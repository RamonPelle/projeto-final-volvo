using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
namespace TechStore.Repository.api
{
    public class PedidoRepository
    {
        private readonly TechStoreContext _context;
        public PedidoRepository(TechStoreContext context) => _context = context;

        public async Task<List<Pedido>> BuscarPorClienteId(int clienteId)
        {
            return await _context.Pedidos
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task<Pedido?> BuscarPorId(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task AdicionarPedido(Pedido pedido)
        {
            if (pedido is null)
                throw new ArgumentNullException(nameof(pedido));

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarPedido(int id)
        {
            await _context.Pedidos
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task EditarPedido(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
