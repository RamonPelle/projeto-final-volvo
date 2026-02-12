using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechStore.Models;
using TechStore.DTOs.Request;
using TechStore.DTOs.Response;
using TechStore.Services.api;
using AutoMapper;

namespace TechStore.Controllers.api
{
    /// <summary>
    /// Controller para operações de Cliente.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
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
        [SwaggerOperation(Summary = "Cria novo cliente", Description = "Adiciona um novo cliente ao sistema. Regras de negócio: o corpo da requisição não pode ser nulo; o e-mail informado deve ser único; a senha é armazenada de forma encriptada; a entidade Cliente deve ser válida conforme as regras de validação.")]
        [SwaggerResponse(201, "Cliente criado com sucesso.", typeof(ClienteResponse))]
        [SwaggerResponse(400, "Erro de validação.")]
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
        [SwaggerOperation(Summary = "Retorna todos os clientes", Description = "Obtém a lista completa de clientes cadastrados")]
        [SwaggerResponse(200, "Lista de clientes retornada com sucesso.", typeof(List<ClienteResponse>))]
        public async Task<ActionResult<List<ClienteResponse>>> BuscarClientes()
        {
            var clientes = await _clienteService.ObterTodosClientes();
            var clientesResponse = _mapper.Map<List<ClienteResponse>>(clientes);
            return Ok(clientesResponse);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Retorna cliente por ID", Description = "Obtém um cliente específico pelo seu ID. Regras de negócio: se o cliente não existir, é retornado erro de não encontrado.")]
        [SwaggerResponse(200, "Cliente encontrado.", typeof(ClienteResponse))]
        [SwaggerResponse(404, "Cliente não encontrado.")]
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
        [SwaggerOperation(Summary = "Deleta cliente por ID", Description = "Remove um cliente específico pelo seu ID. Regras de negócio: o cliente deve existir, caso contrário é retornado erro de não encontrado.")]
        [SwaggerResponse(204, "Cliente deletado com sucesso.")]
        [SwaggerResponse(404, "Cliente não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            await _clienteService.DeletarCliente(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Edita cliente", Description = "Edita um cliente pelo seu ID. Regras de negócio: o corpo da requisição não pode ser nulo; o cliente deve existir; o novo e-mail não pode estar em uso por outro cliente; os dados atualizados devem ser válidos conforme as regras de validação.")]
        [SwaggerResponse(204, "Cliente editado com sucesso.")]
        [SwaggerResponse(404, "Cliente não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> AtualizarCliente(int id, [FromBody] ClientePutRequest request)
        {
            await _clienteService.AtualizarCliente(id, request);
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [SwaggerOperation(Summary = "Atualiza parcialmente cliente", Description = "Atualiza parcialmente os dados de um cliente pelo seu ID (nome, e-mail, telefone). Regras de negócio: o corpo da requisição não pode ser nulo; pelo menos um campo deve ser informado; o cliente deve existir; o novo e-mail não pode estar em uso por outro cliente; os dados atualizados devem ser válidos conforme as regras de validação.")]
        [SwaggerResponse(204, "Cliente atualizado parcialmente com sucesso.")]
        [SwaggerResponse(404, "Cliente não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> AtualizarClienteParcial(int id, [FromBody] ClientePatchRequest request)
        {
            await _clienteService.AtualizarClienteParcial(id, request);
            return NoContent();
        }
    }
}
