using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Repository.api;
using TechStore.Utils;

namespace TechStore.Services.api
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoService(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<Produto>> ObterTodosProdutos()
        {
            return await _produtoRepository.BuscarTodos();
        }

        public async Task<Produto?> BuscarProdutoPorId(int id)
        {
            return await _produtoRepository.BuscarPorId(id);
        }
    }
}