using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechStore.DTOs.Request;
using TechStore.Models.DTOs.Response;
using TechStore.Services.api;
using TechStore.Models.DTOs.Request;

namespace TechStore.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<ProdutoResponse>>> BuscarProdutos([FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string? nome = null, [FromQuery] decimal? precoMin = null, [FromQuery] decimal? precoMax = null)
        {
            var produtos = await _produtoService.ObterTodosProdutos(skip, take, nome, precoMin, precoMax);
            var produtosResponse = _mapper.Map<List<ProdutoResponse>>(produtos);
            return Ok(produtosResponse);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoResponse>> BuscarProdutoPorId(int id)
        {
            var produto = await _produtoService.BuscarProdutoPorId(id);
            var produtoResponse = _mapper.Map<ProdutoResponse>(produto);
            return Ok(produtoResponse);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletarProduto(int id)
        {
            await _produtoService.DeletarProduto(id);
            return NoContent();
        }

        [HttpPost]
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
        public async Task<ActionResult> AtualizarProduto(int id, [FromBody] ProdutoRequest produtoRequest)
        {
            await _produtoService.AtualizarProduto(id, produtoRequest);
            return NoContent();
        }
    }
}
