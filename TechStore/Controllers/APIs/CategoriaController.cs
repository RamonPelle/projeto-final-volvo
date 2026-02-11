using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services.api;
using TechStore.Models.DTOs.Request;
using Swashbuckle.AspNetCore.Annotations;
using TechStore.Models.DTOs.Response;

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
                [SwaggerResponse(200, "Lista de categorias retornada com sucesso.", typeof(List<Categoria>))]
                public async Task<ActionResult<List<CategoriaResponse>>> BuscarCategorias()
                {
                        var categorias = await _categoriaService.ObterTodasCategorias();
                        return Ok(categorias);
                }

                [HttpGet("{id:int}")]
                [SwaggerOperation(Summary = "Retorna categoria por ID", Description = "Obtém uma categoria específica pelo seu ID.")]
                [SwaggerResponse(200, "Categoria encontrada.", typeof(Categoria))]
                [SwaggerResponse(404, "Categoria não encontrada.")]
                public async Task<ActionResult<CategoriaResponse>> BuscarCategoriaPorId(int id)
                {
                        var categoria = await _categoriaService.BuscarCategoriaPorId(id);
                        return Ok(categoria);
                }

                [HttpDelete("{id:int}")]
                [SwaggerOperation(Summary = "Deleta categoria por ID", Description = "Remove uma categoria específica pelo seu ID.")]
                [SwaggerResponse(204, "Categoria deletada com sucesso.")]
                [SwaggerResponse(404, "Categoria não encontrada.")]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<ActionResult> DeletarCategoria(int id)
                {
                        await _categoriaService.DeletarCategoria(id);
                        return NoContent();
                }

                [HttpPost]
                [SwaggerOperation(Summary = "Cria nova categoria", Description = "Adiciona uma nova categoria ao sistema.")]
                [SwaggerResponse(201, "Categoria criada com sucesso.")]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<ActionResult> AdicionarCategoria([FromBody] CategoriaRequest categoriaRequest)
                {
                        var novaCategoria = await _categoriaService.AdicionarCategoria(categoriaRequest);
                        return CreatedAtAction(
                            nameof(BuscarCategorias),
                            new { id = novaCategoria.Id },
                            novaCategoria
                        );
                }

                [HttpPut("{id:int}")]
                [SwaggerOperation(Summary = "Edita categoria", Description = "Edita uma categoria pelo seu ID.")]
                [SwaggerResponse(204, "Categoria editada com sucesso.")]
                [SwaggerResponse(404, "Categoria não encontrada.")]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<ActionResult> AtualizarCategoria(int id, [FromBody] CategoriaRequest categoriaRequest)
                {
                        await _categoriaService.AtualizarCategoria(id, categoriaRequest);
                        return NoContent();
                }

        }
}