using TechStore.Data;
using TechStore.Models;
using Microsoft.EntityFrameworkCore;
namespace TechStore.Repository.api
{
    public class ProdutoRepository
    {
        private readonly TechStoreContext _context;
        public ProdutoRepository(TechStoreContext context) => _context = context;

        public async Task<List<Produto>> BuscarTodosOsProdutos(int skip, int take, string? nome, decimal? precoMin, decimal? precoMax)
        {
            IQueryable<Produto> query = _context.Produtos;

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.Nome.Contains(nome));
            }

            if (precoMin.HasValue)
            {
                query = query.Where(p => p.Preco >= precoMin.Value);
            }

            if (precoMax.HasValue)
            {
                query = query.Where(p => p.Preco <= precoMax.Value);
            }

            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Produto?> BuscarProdutoPorId(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<List<Produto>> BuscarProdutosPorCategoria(int categoriaId)
        {
            return await _context.Produtos
                .Where(p => p.CategoriaId == categoriaId)
                .ToListAsync();
        }

        public async Task DeletarProduto(int id)
        {
            await _context.Produtos.Where(produto => produto.Id == id).ExecuteDeleteAsync();
        }

        public async Task AdicionarProduto(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }
    }
}