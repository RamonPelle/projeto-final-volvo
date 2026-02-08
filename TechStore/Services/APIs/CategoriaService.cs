using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Repository.api;
using TechStore.DTOs.Request;
using TechStore.Utils;

namespace TechStore.Services.api
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaService(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<Categoria>> ObterTodasCategorias()
        {
            return await _categoriaRepository.BuscarTodos();
        }

        public async Task<Categoria?> BuscarCategoriaPorId(int id)
        {
            return await _categoriaRepository.BuscarPorId(id);
        }

        public async Task DeletarCategoria(int id)
        {
            await _categoriaRepository.DeletarCategoria(id);
        }

        public async Task<Categoria> AdicionarCategoria(CategoriaRequest categoriaRequest)
        {
            if (categoriaRequest == null)
            {
                throw new ArgumentNullException(nameof(categoriaRequest), "A categoria não pode ser nula.");
            }

            var categoria = new Categoria
            {
                Nome = categoriaRequest.Nome
            };

            var erros = ValidadorEntidade.Validar(categoria);

            if (erros.Any())
            {
                throw new ValidationException(string.Join("; ", erros));
            }

            await _categoriaRepository.Adicionar(categoria);
            return categoria;
        }

        public async Task EditarCategoria(int id, CategoriaRequest dto)
        {
            var categoria = await _categoriaRepository.BuscarPorId(id);

            if (categoria == null)
                throw new KeyNotFoundException();

            categoria.Nome = dto.Nome;

            var erros = ValidadorEntidade.Validar(categoria);
            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _categoriaRepository.EditarCategoria(categoria);
        }

    }
}