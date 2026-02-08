using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services.api;
using TechStore.DTOs;

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

        [HttpGet("cliente/{clienteId:int}")]
        public async Task<ActionResult<List<Pedido>>> GetPedidosPorCliente(int clienteId)
        {
            var pedidos = await _pedidoService.ObterPedidosPorCliente(clienteId);
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

        [HttpPost("cliente/{clienteId:int}")]
        public async Task<ActionResult> AdicionarPedido(int clienteId, [FromBody] PedidoDTO pedidoDto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(errorMessages);
            }

            try
            {
                var novoPedido = await _pedidoService.AdicionarPedido(clienteId, pedidoDto);

                return CreatedAtAction(
                    nameof(GetPedidoPorId),
                    new { id = novoPedido.Id },
                    novoPedido
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}/status")]
        public async Task<IActionResult> GetStatus(int id)
        {
            try
            {
                var status = await _pedidoService.ObterStatus(id);
                return Ok(status);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditarPedido(int id, [FromBody] PedidoEditarDTO pedidoEditarDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _pedidoService.EditarPedido(id, pedidoEditarDto);
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
