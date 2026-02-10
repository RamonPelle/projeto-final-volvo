using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services.api;
using TechStore.DTOs.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace TechStore.Controllers.api
{
    /// <summary>
    /// Controller para operações de Produto.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todos os produtos", Description = "Obtém a lista completa de produtos cadastrados.")]
        [SwaggerResponse(200, "Lista de produtos retornada com sucesso.", typeof(List<Produto>))]
        public async Task<ActionResult<List<Produto>>> BuscarProdutos([FromQuery] string? nome, [FromQuery] decimal? precoMin, [FromQuery] decimal? precoMax)
        {
            var produtos = await _produtoService.ObterTodosProdutos(nome, precoMin, precoMax);
            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Retorna produto por ID", Description = "Obtém um produto específico pelo seu ID.")]
        [SwaggerResponse(200, "Produto encontrado.", typeof(Produto))]
        [SwaggerResponse(404, "Produto não encontrado.")]
        public async Task<ActionResult<Produto>> BuscarProdutoPorId(int id)
        {
            var produto = await _produtoService.BuscarProdutoPorId(id);
            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deleta produto por ID", Description = "Remove um produto específico pelo seu ID.")]
        [SwaggerResponse(204, "Produto deletado com sucesso.")]
        [SwaggerResponse(404, "Produto não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            await _produtoService.DeletarProduto(id);
            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria novo produto", Description = "Adiciona um novo produto ao sistema.")]
        [SwaggerResponse(201, "Produto criado com sucesso.")]
        [SwaggerResponse(400, "Erro de validação.")]
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
        [SwaggerOperation(Summary = "Edita produto", Description = "Edita um produto pelo seu ID.")]
        [SwaggerResponse(204, "Produto editado com sucesso.")]
        [SwaggerResponse(404, "Produto não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProdutoRequest produtoRequest)
        {
            await _produtoService.AtualizarProduto(id, produtoRequest);
            return NoContent();
        }
    }
}
