using Microsoft.AspNetCore.Mvc;
using TechStore.Data;
using TechStore.Models;
using Microsoft.EntityFrameworkCore;
using TechStore.Services.api;
using TechStore.DTOs;

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
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            var categorias = await _categoriaService.ObterTodasCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoriaPorId(int id)
        {
            var categoria = await _categoriaService.BuscarCategoriaPorId(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpDelete("{id}")]
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
        public async Task<ActionResult> PostCategoria([FromBody] CategoriaDTO categoriaDto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errorMessages);
            }

            try
            {
                var novaCategoria = await _categoriaService.AdicionarCategoria(categoriaDto);
                return CreatedAtAction(nameof(GetCategorias), new { id = novaCategoria.Id }, novaCategoria);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}