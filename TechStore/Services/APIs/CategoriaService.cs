using TechStore.Models;
using TechStore.Repository.api;
using TechStore.DTOs;

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

        public async Task<Categoria> AdicionarCategoria(CategoriaDTO categoriaDto)
        {
            if (categoriaDto == null)
            {
                throw new ArgumentNullException(nameof(categoriaDto), "A categoria não pode ser nula.");
            }

            var categoria = new Categoria
            {
                Nome = categoriaDto.Nome
            };

            await _categoriaRepository.Adicionar(categoria);
            return categoria;
        }
    }
}