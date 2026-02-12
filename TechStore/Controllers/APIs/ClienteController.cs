using Microsoft.AspNetCore.Mvc;
using TechStore.DTOs.Request;
using TechStore.DTOs.Response;
using TechStore.Services.api;
using AutoMapper;

namespace TechStore.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteController(ClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ClienteResponse>> AdicionarCliente([FromBody] ClienteRequest clienteRequest)
        {
            var cliente = await _clienteService.AdicionarCliente(clienteRequest);
            var clienteResponse = _mapper.Map<ClienteResponse>(cliente);
            return CreatedAtAction(
                nameof(BuscarClientePorId),
                new { id = clienteResponse.Id },
                clienteResponse
            );
        }

        [HttpGet]
        public async Task<ActionResult<List<ClienteResponse>>> BuscarClientes()
        {
            var clientes = await _clienteService.ObterTodosClientes();
            var clientesResponse = _mapper.Map<List<ClienteResponse>>(clientes);
            return Ok(clientesResponse);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClienteResponse>> BuscarClientePorId(int id)
        {
            var cliente = await _clienteService.BuscarClientePorId(id);
            if (cliente == null)
            {
                return NotFound();
            }

            var clienteResponse = _mapper.Map<ClienteResponse>(cliente);
            return Ok(clienteResponse);
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
