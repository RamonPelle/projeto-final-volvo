using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Repository.api;
using TechStore.Utils;
using TechStore.DTOs;

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

        public async Task<Produto> AdicionarProduto(ProdutoDTO produtoDto)
        {
            if (produtoDto == null)
                throw new ArgumentNullException(nameof(produtoDto), "O produto não pode ser nulo.");

            var produto = new Produto
            {
                Nome = produtoDto.Nome,
                Preco = produtoDto.Preco,
                Descricao = produtoDto.Descricao, 
                Estoque = produtoDto.Estoque,
                CategoriaId = produtoDto.CategoriaId
            };

            var erros = ValidadorEntidade.Validar(produto);

            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _produtoRepository.Adicionar(produto);
            return produto;
        }
    }
}