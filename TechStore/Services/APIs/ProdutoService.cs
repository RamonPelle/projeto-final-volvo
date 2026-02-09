using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Repository.api;
using TechStore.Utils;
using TechStore.DTOs.Request;
using AutoMapper;

namespace TechStore.Services.api
{
    public class ProdutoService
    {
        private readonly IMapper _mapper;
        private readonly ProdutoRepository _produtoRepository;
        private readonly CategoriaRepository _categoriaRepository;

        public ProdutoService(
            ProdutoRepository produtoRepository,
            CategoriaRepository categoriaRepository,
            IMapper mapper)
                {
                    _produtoRepository = produtoRepository;
                    _categoriaRepository = categoriaRepository;
                    _mapper = mapper;
                }

        public async Task<List<Produto>> ObterTodosProdutos()
        {
            return await _produtoRepository.BuscarTodos();
        }

        public async Task<Produto?> BuscarProdutoPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id deve ser maior que zero.", nameof(id));

            var produto = await _produtoRepository.BuscarPorId(id);

            if (produto == null)
                throw new KeyNotFoundException($"Produto com id {id} não encontrado.");

            return produto;
        }

        public async Task DeletarProduto(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id deve ser maior que zero.", nameof(id));

            var produto = await _produtoRepository.BuscarPorId(id);

            if (produto == null)
                throw new KeyNotFoundException($"Produto com id {id} não encontrado.");

            await _produtoRepository.DeletarProduto(id);
        }

        public async Task<Produto> AdicionarProduto(ProdutoRequest produtoRequest)
        {
            var categoria = await _categoriaRepository.BuscarPorId(produtoRequest.CategoriaId);

            if (categoria == null)
                throw new ValidationException($"Categoria com id {produtoRequest.CategoriaId} não existe.");

            var produto = _mapper.Map<Produto>(produtoRequest);

            var erros = ValidadorEntidade.Validar(produto);

            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _produtoRepository.AdicionarProduto(produto);
            return produto;
        }

        public async Task AtualizarProduto(int id, ProdutoRequest produtoRequest)
        {
            if (produtoRequest == null)
                throw new ArgumentNullException(nameof(produtoRequest), "O produto não pode ser nulo.");

            var produto = await _produtoRepository.BuscarPorId(id);

            if (produto == null)
                throw new KeyNotFoundException($"Produto com id {id} não encontrado.");

            var categoria = await _categoriaRepository.BuscarPorId(produtoRequest.CategoriaId);

            if (categoria == null)
                throw new ValidationException($"Categoria com id {produtoRequest.CategoriaId} não existe.");

            _mapper.Map(produtoRequest, produto);

            var erros = ValidadorEntidade.Validar(produto);
            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _produtoRepository.AtualizarProduto(produto);
        }
    }
}