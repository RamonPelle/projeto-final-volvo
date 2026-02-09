using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services.api;
using TechStore.DTOs.Request;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;
        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> BuscarProdutos()
        {
            var produtos = await _produtoService.ObterTodosProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> BuscarProdutoPorId(int id)
        {
            var produto = await _produtoService.BuscarProdutoPorId(id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            try
            {
                await _produtoService.DeletarProduto(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarProduto([FromBody] ProdutoRequest produtoRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errorMessages);
            }

            try
            {
                var novoProduto = await _produtoService.AdicionarProduto(produtoRequest);
                return CreatedAtAction(nameof(BuscarProdutos), new { id = novoProduto.Id }, novoProduto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProdutoRequest produtoRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errorMessages);
            }

            try
            {
                await _produtoService.AtualizarProduto(id, produtoRequest);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
