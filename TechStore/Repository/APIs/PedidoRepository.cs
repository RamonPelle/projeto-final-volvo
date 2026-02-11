using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
using TechStore.Models.Enums;
using TechStore.Models.DTOs.Response;
using Microsoft.EntityFrameworkCore.Storage;

namespace TechStore.Repository.api
{
    public class PedidoRepository
    {
        private readonly TechStoreContext _context;
        public PedidoRepository(TechStoreContext context) => _context = context;

        public async Task<List<Pedido>> BuscarTodosOsPedidos(int? clienteId, StatusPedido? status)
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

            return await query.Include(p => p.Itens).ThenInclude(i => i.Produto).ToListAsync();
        }

        public async Task<Pedido?> BuscarPedidoPorId(int id)
        {
            return await _context.Pedidos
                                 .Include(p => p.Itens)
                                 .ThenInclude(i => i.Produto)
                                 .FirstOrDefaultAsync(i => i.Id == id);
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

        public async Task FinalizarPedido(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ValorPorCategoriaResponse>> ObterValorTotalVendidoPorCategoria()
        {
            return await _context.ItensPedido
                .Include(i => i.Pedido)
                .Where(i => i.Pedido.Status == StatusPedido.Concluido)
                .Include(i => i.Produto)
                .GroupBy(i => i.Produto.CategoriaId)
                .Select(g => new
                {
                    CategoriaId = g.Key,
                    ValorTotal = g.Sum(i => i.Quantidade * i.PrecoUnitario)
                })
                .Join(_context.Categorias,
                    resultado => resultado.CategoriaId,
                    categoria => categoria.Id,
                    (resultado, categoria) => new ValorPorCategoriaResponse
                    {
                        Categoria = categoria.Nome,
                        ValorTotal = resultado.ValorTotal
                    })
                .ToListAsync();
        }

        public async Task<IDbContextTransaction> IniciarTransacao()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
