using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.Models.DTOs.Request;
using TechStore.Models.DTOs.Response;
using TechStore.Services.api;
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
                private readonly IMapper _mapper;

                public ProdutoController(ProdutoService produtoService, IMapper mapper)
                {
                        _produtoService = produtoService;
                        _mapper = mapper;
                }

                [HttpGet]
                [SwaggerOperation(Summary = "Retorna todos os produtos", Description = "Obtém a lista completa de produtos cadastrados.")]
                [SwaggerResponse(200, "Lista de produtos retornada com sucesso.", typeof(List<Produto>))]
                public async Task<ActionResult<List<ProdutoResponse>>> BuscarProdutos([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string? nome = null, [FromQuery] decimal? precoMin = null, [FromQuery] decimal? precoMax = null)
                {
                        var produtos = await _produtoService.ObterTodosProdutos(skip, take, nome, precoMin, precoMax);
                        var produtosResponse = _mapper.Map<List<ProdutoResponse>>(produtos);
                        return Ok(produtosResponse);
                }

                [HttpGet("{id:int}")]
                [SwaggerOperation(Summary = "Retorna produto por ID", Description = "Obtém um produto específico pelo seu ID.")]
                [SwaggerResponse(200, "Produto encontrado.", typeof(Produto))]
                [SwaggerResponse(404, "Produto não encontrado.")]
                public async Task<ActionResult<ProdutoResponse>> BuscarProdutoPorId(int id)
                {
                        var produto = await _produtoService.BuscarProdutoPorId(id);
                        var produtoResponse = _mapper.Map<ProdutoResponse>(produto);
                        return Ok(produtoResponse);
                }

                [HttpDelete("{id:int}")]
                [SwaggerOperation(Summary = "Deleta produto por ID", Description = "Remove um produto específico pelo seu ID.")]
                [SwaggerResponse(204, "Produto deletado com sucesso.")]
                [SwaggerResponse(404, "Produto não encontrado.")]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<ActionResult> DeletarProduto(int id)
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
                        var produtoResponse = _mapper.Map<ProdutoResponse>(produto);

                        return CreatedAtAction(
                            nameof(BuscarProdutoPorId),
                            new { id = produto.Id },
                            produtoResponse
                        );
                }

                [HttpPut("{id:int}")]
                [SwaggerOperation(Summary = "Edita produto", Description = "Edita um produto pelo seu ID.")]
                [SwaggerResponse(204, "Produto editado com sucesso.")]
                [SwaggerResponse(404, "Produto não encontrado.")]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<ActionResult> AtualizarProduto(int id, [FromBody] ProdutoRequest produtoRequest)
                {
                        await _produtoService.AtualizarProduto(id, produtoRequest);
                        return NoContent();
                }

                [HttpPatch("{id:int}/estoque/{qtd:int}")]
                public async Task<IActionResult> AtualizarEstoqueProdutos(int id, int qtd)
                {
                        await _produtoService.AtualizarEstoqueProdutos(id, qtd);
                        return NoContent();
                }
        }
}
