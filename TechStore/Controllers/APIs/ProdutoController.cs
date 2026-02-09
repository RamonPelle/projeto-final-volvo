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
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<List<Produto>>> GetProdutos()
        {
            var produtos = await _produtoService.ObterTodosProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retorna produto por ID", Description = "Obtém um produto específico pelo seu ID.")]
        [SwaggerResponse(200, "Produto encontrado.", typeof(Produto))]
        [SwaggerResponse(404, "Produto não encontrado.")]
        public async Task<ActionResult<Produto>> GetProdutoPorId(int id)
        {
            var produto = await _produtoService.BuscarProdutoPorId(id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta produto por ID", Description = "Remove um produto específico pelo seu ID.")]
        [SwaggerResponse(204, "Produto deletado com sucesso.")]
        [SwaggerResponse(404, "Produto não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            try
            {
                await _produtoService.DeletarProduto(id);
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
        [SwaggerOperation(Summary = "Cria novo produto", Description = "Adiciona um novo produto ao sistema.")]
        [SwaggerResponse(201, "Produto criado com sucesso.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<ActionResult> PostProduto([FromBody] ProdutoRequest produtoRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errorMessages);
            }

            try
            {
                var novoProduto = await _produtoService.AdicionarProduto(produtoRequest);
                return CreatedAtAction(nameof(GetProdutos), new { id = novoProduto.Id }, novoProduto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Edita produto", Description = "Edita um produto pelo seu ID.")]
        [SwaggerResponse(204, "Produto editado com sucesso.")]
        [SwaggerResponse(404, "Produto não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> EditarProduto(int id, [FromBody] ProdutoRequest produtoRequest)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errorMessages);
            }

            try
            {
                await _produtoService.EditarProduto(id, produtoRequest);
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
