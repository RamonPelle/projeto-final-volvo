using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services.api;
using TechStore.DTOs.Request;

namespace TechStore.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> BuscarProdutos([FromQuery] string? nome, [FromQuery] decimal? precoMin, [FromQuery] decimal? precoMax)
        {
            var produtos = await _produtoService.ObterTodosProdutos(nome, precoMin, precoMax);
            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> BuscarProdutoPorId(int id)
        {
            var produto = await _produtoService.BuscarProdutoPorId(id);
            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            await _produtoService.DeletarProduto(id);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarProduto([FromBody] ProdutoRequest produtoRequest)
        {
            var produto = await _produtoService.AdicionarProduto(produtoRequest);

            return CreatedAtAction(
                nameof(BuscarProdutoPorId),
                new { id = produto.Id },
                produto
            );
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProdutoRequest produtoRequest)
        {
            await _produtoService.AtualizarProduto(id, produtoRequest);
            return NoContent();
        }
    }
}
