using Microsoft.AspNetCore.Mvc;
using TechStore.Services.api;
using TechStore.DTOs.Request;
using TechStore.DTOs.Response;
using TechStore.Models.Enums;
using AutoMapper;

namespace TechStore.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;
        private readonly ItemPedidoService _itemPedidoService;
        private readonly IMapper _mapper;

        public PedidoController(
            PedidoService pedidoService,
            ItemPedidoService itemPedidoService,
            IMapper mapper
        )
        {
            _pedidoService = pedidoService;
            _itemPedidoService = itemPedidoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoResponse>>> BuscarPedidos(
            [FromQuery] int? clienteId,
            [FromQuery] StatusPedido? status
        )
        {
            var pedidos = await _pedidoService.BuscarTodosOsPedidos(clienteId, status);
            var resposta = _mapper.Map<IEnumerable<PedidoResponse>>(pedidos);

            return Ok(resposta);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PedidoResponse>> BuscarPedidoPorId(int id)
        {
            var pedido = await _pedidoService.BuscarPedidoPorId(id);

            if (pedido is null)
                return NotFound();

            return Ok(_mapper.Map<PedidoResponse>(pedido));
        }

        [HttpGet("valor-total-por-categoria")]
        public async Task<ActionResult<IEnumerable<ValorPorCategoriaResponse>>> ObterValorTotalVendidoPorCategoria()
        {
            var resultado = await _pedidoService.ObterValorTotalVendidoPorCategoria();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<PedidoResponse>> CriarPedido(
           [FromBody] PedidoRequest pedidoRequest
        )
        {
            var pedido = await _pedidoService.CriarPedido(pedidoRequest);
            var response = _mapper.Map<PedidoResponse>(pedido);

            return CreatedAtAction(
                nameof(BuscarPedidoPorId),
                new { id = pedido.Id },
                response
            );
        }

        [HttpPatch("{id:int}/finalizar")]
        public async Task<IActionResult> FinalizarPedido(int id)
        {
            await _pedidoService.FinalizarPedido(id);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletarPedido(int id)
        {
            await _pedidoService.DeletarPedido(id);
            return NoContent();
        }

        [HttpDelete("{id:int}/item/{itemId:int}")]
        public async Task<IActionResult> DeletarItem(int id, int itemId)
        {
            await _itemPedidoService.DeletarItem(id, itemId);
            return NoContent();
        }
    }
}
