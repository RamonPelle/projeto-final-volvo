using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Repository.api
{
    public class ClienteRepository
    {
        private readonly TechStoreContext _context;

        public ClienteRepository(TechStoreContext context) => _context = context;

        public async Task AdicionarCliente(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cliente>> BuscarTodosClientes()
        {
            //return await _context.Clientes.Include(cliente => cliente.Pedidos).ToListAsync();
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> BuscarClientePorNome(string nome)
        {
            //return await _context.Clientes.Include(cliente => cliente.Pedidos).FirstOrDefaultAsync(cliente => cliente.Nome == nome);
            return await _context.Clientes.FirstOrDefaultAsync(cliente => cliente.Nome == nome);
        }

        public async Task<Cliente?> BuscarClientePorEmail(string email)
        {
            //return await _context.Clientes.Include(cliente => cliente.Pedidos).FirstOrDefaultAsync(cliente => cliente.Email == email);
            return await _context.Clientes.FirstOrDefaultAsync(cliente => cliente.Email == email);
        }

        public async Task<Cliente?> BuscarClientePorId(int id)
        {
            //return await _context.Clientes.Include(cliente => cliente.Pedidos).FirstOrDefaultAsync(cliente => cliente.Id == id);
            return await _context.Clientes.FirstOrDefaultAsync(cliente => cliente.Id == id);
        }

        public async Task DeletarCliente(int id)
        {
            await _context.Clientes.Where(cliente => cliente.Id == id).ExecuteDeleteAsync();
        }

        public async Task AtualizarCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

    }
}
