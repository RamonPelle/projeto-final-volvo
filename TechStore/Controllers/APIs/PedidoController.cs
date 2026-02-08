using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services.api;
using TechStore.DTOs.Request;
using TechStore.DTOs.Response;
using TechStore.Models.Enums;

namespace TechStore.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;

        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // TODO GET /api/pedido? clientId = 123 & status = pending
        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> GetPedidosPorCliente([FromQuery] int clienteId, [FromQuery] StatusPedido status)
        {
            var pedidos = await _pedidoService.ObterPedidos(clienteId, status);
            return Ok(pedidos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pedido>> GetPedidoPorId(int id)
        {
            var pedido = await _pedidoService.BuscarPedidoPorId(id);

            if (pedido is null)
                return NotFound();

            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult<PedidoResponse>> CriarPedido([FromBody] PedidoRequest pedidoRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var pedido = await _pedidoService.CriarPedido(pedidoRequest);

                var resposta = new PedidoResponse
                {
                    Id = pedido.Id,
                    Itens = pedido.Itens.Select(i => new PedidoResponse.ItensResponse
                    {
                        Id = i.Id,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario
                    }).ToList()
                };

                return CreatedAtAction(
                    nameof(GetPedidoPorId),
                    new { id = pedido.Id },
                    resposta
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditarPedido(int id, [FromBody] PedidoEditarRequest pedidoEditarRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _pedidoService.EditarPedido(id, pedidoEditarRequest);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarPedido(int id)
        {
            try
            {
                await _pedidoService.DeletarPedido(id);
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
