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
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;
        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetProdutos()
        {
            var produtos = await _produtoService.ObterTodosProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProdutoPorId(int id)
        {
            var produto = await _produtoService.BuscarProdutoPorId(id);

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }


    }

}
