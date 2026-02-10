using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
namespace TechStore.Repository.api
{
    public class CategoriaRepository
    {
        private readonly TechStoreContext _context;
        public CategoriaRepository(TechStoreContext context) => _context = context;

        public async Task<List<Categoria>> BuscarTodasAsCategorias()
            => await _context.Categorias.ToListAsync();

        public async Task AdicionarCategoria(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<Categoria?> BuscarCategoriaPorId(int id)
        {
            return await _context.Categorias.Include(produto => produto.Produtos).FirstOrDefaultAsync(categoria =>
                    categoria.Id == id
                );
        }

        public async Task DeletarCategoria(int id)
        {
            await _context.Categorias.Where(categoria => categoria.Id == id).ExecuteDeleteAsync();
        }

        public async Task AtualizarCategoria(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }
    }
}