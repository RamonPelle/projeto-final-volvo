using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.Data
{
    public class TechStoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=TechStoreDB;User Id=sa;Password=1234;TrustServerCertificate=True");
        }

        public DbSet<Categoria> Categorias { get; set; } = null;
        public DbSet<Produto> Produtos { get; set; } = null;
        public DbSet<Cliente> Clientes { get; set; } = null;
        public DbSet<Pedido> Pedidos { get; set; } = null;
        public DbSet<ItemPedido> ItensPedido { get; set; } = null;

    }
}