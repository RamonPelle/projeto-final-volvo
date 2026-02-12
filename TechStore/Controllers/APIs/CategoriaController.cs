using Microsoft.AspNetCore.Mvc;
using TechStore.Services.api;
using TechStore.DTOs.Request;
using Swashbuckle.AspNetCore.Annotations;
using TechStore.DTOs.Response;

namespace TechStore.Controllers.api
{
        /// <summary>
        /// Controller para operações de Categoria.
        /// </summary>
        [Route("api/[controller]")]
        [ApiController]
        [Produces("application/json")]
        [Consumes("application/json")]
        public class CategoriaController : ControllerBase
        {
                private readonly CategoriaService _categoriaService;

                public CategoriaController(CategoriaService categoriaService)
                {
                        _categoriaService = categoriaService;
                }

                [HttpGet]
                [SwaggerOperation(Summary = "Retorna todas as categorias", Description = "Obtém a lista completa de categorias cadastradas.")]
                [SwaggerResponse(200, "Lista de categorias retornada com sucesso.", typeof(List<CategoriaResponse>))]
                public async Task<ActionResult<List<CategoriaResponse>>> BuscarCategorias()
                {
                        var categorias = await _categoriaService.ObterTodasCategorias();
                        return Ok(categorias);
                }

                [HttpGet("{id:int}")]
                [SwaggerOperation(Summary = "Retorna categoria por ID", Description = "Obtém uma categoria específica pelo seu ID. Regras de negócio: o ID deve ser maior que zero; caso a categoria não exista, um erro será retornado.")]
                [SwaggerResponse(200, "Categoria encontrada.", typeof(CategoriaResponse))]
                [SwaggerResponse(404, "Categoria não encontrada.")]
                public async Task<ActionResult<CategoriaResponse>> BuscarCategoriaPorId(int id)
                {
                        var categoria = await _categoriaService.BuscarCategoriaPorId(id);
                        return Ok(categoria);
                }

                [HttpDelete("{id:int}")]
                [SwaggerOperation(Summary = "Deleta categoria por ID", Description = "Remove uma categoria específica pelo seu ID. Regras de negócio: o ID deve ser maior que zero; a categoria deve existir e não pode possuir produtos associados.")]
                [SwaggerResponse(204, "Categoria deletada com sucesso.")]
                [SwaggerResponse(404, "Categoria não encontrada.")]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<IActionResult> DeletarCategoria(int id)
                {
                        await _categoriaService.DeletarCategoria(id);
                        return NoContent();
                }

                [HttpPost]
                [SwaggerOperation(Summary = "Cria nova categoria", Description = "Adiciona uma nova categoria ao sistema. Regras de negócio: o corpo da requisição não pode ser nulo e a entidade deve ser válida conforme as anotações de validação.")]
                [SwaggerResponse(201, "Categoria criada com sucesso.", typeof(CategoriaResponse))]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<ActionResult<CategoriaResponse>> AdicionarCategoria([FromBody] CategoriaRequest categoriaRequest)
                {
                        var novaCategoria = await _categoriaService.AdicionarCategoria(categoriaRequest);
                        var novaCategoriaResponse = new CategoriaResponse
                        {
                                Id = novaCategoria.Id,
                                Nome = novaCategoria.Nome
                        };

                        return CreatedAtAction(
                            nameof(BuscarCategoriaPorId),
                            new { id = novaCategoriaResponse.Id },
                            novaCategoriaResponse
                        );
                }

                [HttpPut("{id:int}")]
                [SwaggerOperation(Summary = "Edita categoria", Description = "Edita uma categoria pelo seu ID. Regras de negócio: o corpo da requisição não pode ser nulo; a categoria deve existir; os dados atualizados devem ser válidos conforme as anotações de validação.")]
                [SwaggerResponse(204, "Categoria editada com sucesso.")]
                [SwaggerResponse(404, "Categoria não encontrada.")]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<IActionResult> AtualizarCategoria(int id, [FromBody] CategoriaRequest categoriaRequest)
                {
                        await _categoriaService.AtualizarCategoria(id, categoriaRequest);
                        return NoContent();
                }

        }
}