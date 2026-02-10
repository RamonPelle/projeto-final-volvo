using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services.api;
using TechStore.DTOs.Request;

namespace TechStore.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> BuscarCategorias()
        {
            var categorias = await _categoriaService.ObterTodasCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Categoria>> BuscarCategoriaPorId(int id)
        {
            var categoria = await _categoriaService.BuscarCategoriaPorId(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
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
        public async Task<ActionResult> AdicionarCategoria([FromBody] CategoriaRequest categoriaRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errorMessages);
            }

            try
            {
                var novaCategoria = await _categoriaService.AdicionarCategoria(categoriaRequest);
                return CreatedAtAction(nameof(BuscarCategorias), new { id = novaCategoria.Id }, novaCategoria);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarCategoria(int id, [FromBody] CategoriaRequest categoriaRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errorMessages);
            }

            try
            {
                await _categoriaService.AtualizarCategoria(id, categoriaRequest);
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