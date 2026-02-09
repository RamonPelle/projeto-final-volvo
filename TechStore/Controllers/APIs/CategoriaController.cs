using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services.api;
using TechStore.DTOs.Request;
using Swashbuckle.AspNetCore.Annotations;

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
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            var categorias = await _categoriaService.ObterTodasCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Retorna categoria por ID", Description = "Obtém uma categoria específica pelo seu ID.")]
        [SwaggerResponse(200, "Categoria encontrada.", typeof(Categoria))]
        [SwaggerResponse(404, "Categoria não encontrada.")]
        public async Task<ActionResult<Categoria>> GetCategoriaPorId(int id)
        {
            var categoria = await _categoriaService.BuscarCategoriaPorId(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deleta categoria por ID", Description = "Remove uma categoria específica pelo seu ID.")]
        [SwaggerResponse(204, "Categoria deletada com sucesso.")]
        [SwaggerResponse(404, "Categoria não encontrada.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> DeletarCategoria(int id)
        {
            try
            {
                await _categoriaService.DeletarCategoria(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria nova categoria", Description = "Adiciona uma nova categoria ao sistema.")]
        [SwaggerResponse(201, "Categoria criada com sucesso.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<ActionResult> PostCategoria([FromBody] CategoriaRequest categoriaRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errorMessages);
            }

            try
            {
                var novaCategoria = await _categoriaService.AdicionarCategoria(categoriaRequest);
                return CreatedAtAction(nameof(GetCategorias), new { id = novaCategoria.Id }, novaCategoria);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Edita categoria", Description = "Edita uma categoria pelo seu ID.")]
        [SwaggerResponse(204, "Categoria editada com sucesso.")]
        [SwaggerResponse(404, "Categoria não encontrada.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> EditarCategoria(int id, [FromBody] CategoriaRequest categoriaRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errorMessages);
            }

            try
            {
                await _categoriaService.EditarCategoria(id, categoriaRequest);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}