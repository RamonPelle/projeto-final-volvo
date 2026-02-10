using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Repository.api;
using TechStore.DTOs.Request;
using TechStore.Utils;
using AutoMapper;

namespace TechStore.Services.api
{
    public class CategoriaService
    {
        private readonly IMapper _mapper;
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaService(
            CategoriaRepository categoriaRepository,
            IMapper mapper)
                {
                    _categoriaRepository = categoriaRepository;
                    _mapper = mapper;
                }

        public async Task<List<Categoria>> ObterTodasCategorias()
        {
            return await _categoriaRepository.BuscarTodasAsCategorias();
        }

        public async Task<Categoria?> BuscarCategoriaPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id deve ser maior que zero.", nameof(id));

            var categoria =  await _categoriaRepository.BuscarCategoriaPorId(id);

            if (categoria == null)
                throw new KeyNotFoundException($"Categoria com id {id} não encontrada.");

            return categoria;
        }

        public async Task DeletarCategoria(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id deve ser maior que zero.", nameof(id));

            var categoria = await _categoriaRepository.BuscarCategoriaPorId(id);

            if (categoria == null)
                throw new KeyNotFoundException($"Categoria com id {id} não encontrada.");

            if (categoria.Produtos.Any())
            {
                throw new ValidationException("A categoria não pode ser removida pois possui produtos associados a ela.");
            }

            await _categoriaRepository.DeletarCategoria(id);
        }

        public async Task<Categoria> AdicionarCategoria(CategoriaRequest categoriaRequest)
        {
            if (categoriaRequest == null)
            {
                throw new ArgumentNullException(nameof(categoriaRequest), "A categoria não pode ser nula.");
            }

            var categoria = _mapper.Map<Categoria>(categoriaRequest);

            var erros = ValidadorEntidade.Validar(categoria);

            if (erros.Any())
            {
                throw new ValidationException(string.Join("; ", erros));
            }

            await _categoriaRepository.AdicionarCategoria(categoria);
            return categoria;
        }

        public async Task AtualizarCategoria(int id, CategoriaRequest categoriaRequest)
        {
            if (categoriaRequest == null)
                throw new ArgumentNullException(nameof(categoriaRequest), "A categoria não pode ser nula.");

            var categoria = await _categoriaRepository.BuscarCategoriaPorId(id);

            if (categoria == null)
                throw new KeyNotFoundException($"Cateoria com id {id} não encontrada.");

            _mapper.Map(categoriaRequest, categoria);

            var erros = ValidadorEntidade.Validar(categoria);
            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _categoriaRepository.AtualizarCategoria(categoria);
        }

    }
}