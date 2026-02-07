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
    }

    
}