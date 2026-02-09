using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Repository.api;
using TechStore.Utils;
using TechStore.DTOs.Request;

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

        public async Task DeletarProduto(int id)
        {
            await _produtoRepository.DeletarProduto(id);
        }

        public async Task<Produto> AdicionarProduto(ProdutoRequest produtoRequest)
        {
            if (produtoRequest == null)
                throw new ArgumentNullException(nameof(produtoRequest), "O produto não pode ser nulo.");

            var produto = new Produto
            {
                Nome = produtoRequest.Nome,
                Preco = produtoRequest.Preco,
                Descricao = produtoRequest.Descricao,
                Estoque = produtoRequest.Estoque,
                CategoriaId = produtoRequest.CategoriaId
            };

            var erros = ValidadorEntidade.Validar(produto);

            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _produtoRepository.Adicionar(produto);
            return produto;
        }

        public async Task EditarProduto(int id, ProdutoRequest produtoRequest)
        {
            var produto = await _produtoRepository.BuscarPorId(id);

            if (produto == null)
                throw new KeyNotFoundException();

            produto.Nome = produtoRequest.Nome;

            var erros = ValidadorEntidade.Validar(produto);
            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _produtoRepository.EditarProduto(produto);
        }
    }
}