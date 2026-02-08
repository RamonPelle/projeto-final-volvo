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

        // TODO GET /api/pedido? clientId = 123 & status = pending
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
        public async Task<ActionResult> CriarOuObterPedido(int clienteId, [FromBody] PedidoDTO pedidoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var (pedido, criado) =
                    await _pedidoService.CriarOuObterPedido(clienteId, pedidoDto);

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

                if (criado)
                {
                    return CreatedAtAction(
                        nameof(GetPedidoPorId),
                        new { id = pedido.Id },
                        resposta
                    );
                }

                return Ok(resposta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
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
