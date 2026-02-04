using Microsoft.EntityFrameworkCore;
using TechStore.Models;

namespace TechStore.Data
{
    public class TechStoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=TechStoreDB;User Id=sa; Password=0403; TrustServiceCertificate=True");
        }

        public DbSet<Categoria> Categorias { get; set; } = null;
        public DbSet<Produto> Produtos { get; set; } = null;

    }
}