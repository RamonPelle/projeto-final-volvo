using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.DTOs.Request;
using TechStore.DTOs.Response;
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
                [SwaggerOperation(Summary = "Retorna todos os produtos", Description = "Obtém a lista completa de produtos cadastrados. Permite paginação e filtros por nome e faixa de preço.")]
                [SwaggerResponse(200, "Lista de produtos retornada com sucesso.", typeof(List<ProdutoResponse>))]
                public async Task<ActionResult<List<ProdutoResponse>>> BuscarProdutos([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string? nome = null, [FromQuery] decimal? precoMin = null, [FromQuery] decimal? precoMax = null)
                {
                        var produtos = await _produtoService.ObterTodosProdutos(skip, take, nome, precoMin, precoMax);
                        var produtosResponse = _mapper.Map<List<ProdutoResponse>>(produtos);
                        return Ok(produtosResponse);
                }

                [HttpGet("{id:int}")]
                [SwaggerOperation(Summary = "Retorna produto por ID", Description = "Obtém um produto específico pelo seu ID. Regras de negócio: o ID deve ser maior que zero; se o produto não existir, um erro será retornado.")]
                [SwaggerResponse(200, "Produto encontrado.", typeof(ProdutoResponse))]
                [SwaggerResponse(404, "Produto não encontrado.")]
                public async Task<ActionResult<ProdutoResponse>> BuscarProdutoPorId(int id)
                {
                        var produto = await _produtoService.BuscarProdutoPorId(id);
                        var produtoResponse = _mapper.Map<ProdutoResponse>(produto);
                        return Ok(produtoResponse);
                }

                [HttpGet("categoria/{categoriaId:int}")]
                public async Task<ActionResult<List<ProdutoResponse>>> BuscarProdutosPorCategoria(int categoriaId)
                {
                        var produtos = await _produtoService.BuscarProdutosPorCategoria(categoriaId);

                        var produtosResponse = _mapper.Map<List<ProdutoResponse>>(produtos);

                        return Ok(produtosResponse);
                }

                [HttpDelete("{id:int}")]
                [SwaggerOperation(Summary = "Deleta produto por ID", Description = "Remove um produto específico pelo seu ID. Regras de negócio: o ID deve ser maior que zero; o produto deve existir.")]
                [SwaggerResponse(204, "Produto deletado com sucesso.")]
                [SwaggerResponse(404, "Produto não encontrado.")]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<IActionResult> DeletarProduto(int id)
                {
                        await _produtoService.DeletarProduto(id);
                        return NoContent();
                }

                [HttpPost]
                [SwaggerOperation(Summary = "Cria novo produto", Description = "Adiciona um novo produto ao sistema. Regras de negócio: o corpo da requisição não pode ser nulo; a categoria informada deve existir; o produto deve ser válido conforme as regras de validação da entidade.")]
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
                [SwaggerOperation(Summary = "Edita produto", Description = "Edita um produto pelo seu ID. Regras de negócio: o corpo da requisição não pode ser nulo; o produto deve existir; a categoria informada deve existir; os dados atualizados devem ser válidos conforme as regras de validação da entidade.")]
                [SwaggerResponse(204, "Produto editado com sucesso.")]
                [SwaggerResponse(404, "Produto não encontrado.")]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProdutoRequest produtoRequest)
                {
                        await _produtoService.AtualizarProduto(id, produtoRequest);
                        return NoContent();
                }

                [HttpPatch("{id:int}/estoque/{qtd:int}")]
                [SwaggerOperation(Summary = "Atualiza estoque do produto", Description = "Atualiza o estoque de um produto pelo seu ID. Regras de negócio: o produto deve existir; o novo estoque não pode ficar negativo, caso contrário é gerado erro informando quantidade superior ao estoque disponível.")]
                [SwaggerResponse(204, "Estoque do produto atualizado com sucesso.")]
                [SwaggerResponse(404, "Produto não encontrado.")]
                [SwaggerResponse(400, "Erro de validação.")]
                public async Task<IActionResult> AtualizarEstoqueProdutos(int id, int qtd)
                {
                        await _produtoService.AtualizarEstoqueProdutos(id, qtd);
                        return NoContent();
                }
        }
}
