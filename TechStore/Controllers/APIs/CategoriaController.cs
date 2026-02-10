using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services.api;
using TechStore.DTOs.Request;
using TechStore.Models.DTOs.Response;

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
        public async Task<ActionResult<List<CategoriaResponse>>> BuscarCategorias()
        {
            var categorias = await _categoriaService.ObterTodasCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaResponse>> BuscarCategoriaPorId(int id)
        {
            var categoria = await _categoriaService.BuscarCategoriaPorId(id);
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarCategoria(int id)
        {
            await _categoriaService.DeletarCategoria(id);
            return NoContent();
        }

        [HttpPost]
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
        public async Task<IActionResult> AtualizarCategoria(int id, [FromBody] CategoriaRequest categoriaRequest)
        {
            await _categoriaService.AtualizarCategoria(id, categoriaRequest);
            return NoContent();
        }

    }
}