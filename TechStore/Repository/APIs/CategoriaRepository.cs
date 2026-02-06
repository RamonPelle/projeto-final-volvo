using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;
namespace TechStore.Repository.api
{
    public class CategoriaRepository
    {
        private readonly TechStoreContext _context;
        public CategoriaRepository(TechStoreContext context) => _context = context;

        public async Task<List<Categoria>> BuscarTodos()
            => await _context.Categorias.ToListAsync();

        public async Task Adicionar(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<Categoria?> BuscarPorId(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task DeletarCategoria(int id)
        {
            await _context.Categorias.Where(c => c.Id == id).ExecuteDeleteAsync();
        }

        public async Task EditarCategoria(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }
    }
}