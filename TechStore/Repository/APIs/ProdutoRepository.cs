using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
namespace TechStore.Repository.api
{
    public class ProdutoRepository
    {
        private readonly TechStoreContext _context;
        public ProdutoRepository(TechStoreContext context) => _context = context;

        public async Task<List<Produto>> BuscarTodos()
            => await _context.Produtos.ToListAsync();

        public async Task<Produto?> BuscarPorId(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task DeletarProduto(int id)
        {
            await _context.Produtos.Where(p => p.Id == id).ExecuteDeleteAsync();
        }

        public async Task Adicionar(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task EditarProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }
    }
}