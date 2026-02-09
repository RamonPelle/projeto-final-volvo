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
        private readonly CategoriaRepository _categoriaRepository;

        public ProdutoService(ProdutoRepository produtoRepository, CategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
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

            var categoria = await _categoriaRepository.BuscarPorId(produtoRequest.CategoriaId);

            if (categoria == null)
                throw new ValidationException($"Categoria com id {produtoRequest.CategoriaId} não existe.");

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

            await _produtoRepository.AdicionarProduto(produto);
            return produto;
        }

        public async Task AtualizarProduto(int id, ProdutoRequest produtoRequest)
        {
            var produto = await _produtoRepository.BuscarPorId(id);

            if (produto == null)
                throw new KeyNotFoundException();

            var categoria = await _categoriaRepository.BuscarPorId(produtoRequest.CategoriaId);

            if (categoria == null)
                throw new ValidationException($"Categoria com id {produtoRequest.CategoriaId} não existe.");

            produto.Nome = produtoRequest.Nome;
            produto.Preco = produtoRequest.Preco;
            produto.Descricao = produtoRequest.Descricao;
            produto.Estoque = produtoRequest.Estoque;
            produto.CategoriaId = produtoRequest.CategoriaId;

            var erros = ValidadorEntidade.Validar(produto);
            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _produtoRepository.AtualizarProduto(produto);
        }
    }
}