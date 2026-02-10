using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Repository.api
{
    public class ClienteRepository
    {
        private readonly TechStoreContext _context;

        public ClienteRepository(TechStoreContext context) => _context = context;

        public async Task Adicionar(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cliente>> BuscarTodos()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> BuscarPorNome(string nome)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Nome == nome);
        }

        public async Task<Cliente?> BuscarPorEmail(string email)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Cliente?> BuscarPorId(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task Deletar(int id)
        {
            await _context.Clientes.Where(c => c.Id == id).ExecuteDeleteAsync();
        }

        public async Task Atualizar(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

    }
}
