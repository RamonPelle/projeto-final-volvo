using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
using TechStore.Models.Enums;

namespace TechStore.Repository.api
{
    public class PedidoRepository
    {
        private readonly TechStoreContext _context;
        public PedidoRepository(TechStoreContext context) => _context = context;

        public async Task<List<Pedido>> BuscarTodos(int? clienteId, StatusPedido? status)
        {
            IQueryable<Pedido> query = _context.Pedidos;

            if (clienteId.HasValue)
            {
                query = query.Where(p => p.ClienteId == clienteId);
            }

            if (status.HasValue)
            {
                query = query.Where(p => p.Status == status);
            }

            return await query.Include(p => p.Itens).ToListAsync();
        }

        public async Task<Pedido?> BuscarPorId(int id)
        {
            return await _context.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Pedido?> ObterPedidoAtivoPorCliente(int clienteId)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p =>
                    p.ClienteId == clienteId &&
                    p.Status == StatusPedido.Pendente
                );
        }

        public async Task CriarPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarPedido(int id)
        {
            await _context.Pedidos
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task DeletarItem(int id)
        {
            await _context.ItensPedido.Where(i => i.Id == id).ExecuteDeleteAsync();
        }

        public async Task EditarPedido(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
