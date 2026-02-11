using Microsoft.AspNetCore.Mvc;
using TechStore.Models.DTOs.Request;
using TechStore.Services.api;

namespace TechStore.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarCliente([FromBody] ClienteRequest clienteRequest)
        {
            var cliente = await _clienteService.AdicionarCliente(clienteRequest);
            return CreatedAtAction(
                nameof(BuscarClientePorId),
                new { id = cliente.Id },
                cliente
            );
        }

        [HttpGet]
        public async Task<ActionResult> BuscarClientes()
        {
            var clientes = await _clienteService.ObterTodosClientes();
            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> BuscarClientePorId(int id)
        {
            var cliente = await _clienteService.BuscarClientePorId(id);
            return Ok(cliente);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            await _clienteService.DeletarCliente(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromBody] ClienteEditarRequest request)
        {
            await _clienteService.AtualizarCliente(id, request);
            return NoContent();
        }
    }
}
