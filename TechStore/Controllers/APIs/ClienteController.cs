using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechStore.Models;
using TechStore.Models.DTOs.Request;
using TechStore.Services.api;

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

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria novo cliente", Description = "Adiciona um novo cliente ao sistema. Regras de negócio: o corpo da requisição não pode ser nulo; o e-mail informado deve ser único; a senha é armazenada de forma encriptada; a entidade Cliente deve ser válida conforme as regras de validação.")]
        [SwaggerResponse(201, "Cliente criado com sucesso.", typeof(Cliente))]
        [SwaggerResponse(400, "Erro de validação.")]
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
        [SwaggerOperation(Summary = "Retorna todos os clientes", Description = "Obtém a lista completa de clientes cadastrados")]
        [SwaggerResponse(200, "Lista de clientes retornada com sucesso.", typeof(List<Cliente>))]
        public async Task<ActionResult> BuscarClientes()
        {
            var clientes = await _clienteService.ObterTodosClientes();
            return Ok(clientes);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Retorna cliente por ID", Description = "Obtém um cliente específico pelo seu ID. Regras de negócio: se o cliente não existir, é retornado erro de não encontrado.")]
        [SwaggerResponse(200, "Cliente encontrado.", typeof(Cliente))]
        [SwaggerResponse(404, "Cliente não encontrado.")]
        public async Task<ActionResult> BuscarClientePorId(int id)
        {
            var cliente = await _clienteService.BuscarClientePorId(id);
            return Ok(cliente);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deleta cliente por ID", Description = "Remove um cliente específico pelo seu ID. Regras de negócio: o cliente deve existir, caso contrário é retornado erro de não encontrado.")]
        [SwaggerResponse(204, "Cliente deletado com sucesso.")]
        [SwaggerResponse(404, "Cliente não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<ActionResult> DeletarCliente(int id)
        {
            await _clienteService.DeletarCliente(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Edita cliente", Description = "Edita um cliente pelo seu ID. Regras de negócio: o corpo da requisição não pode ser nulo; o cliente deve existir; o novo e-mail não pode estar em uso por outro cliente; os dados atualizados devem ser válidos conforme as regras de validação.")]
        [SwaggerResponse(204, "Cliente editado com sucesso.")]
        [SwaggerResponse(404, "Cliente não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<ActionResult> AtualizarCliente(int id, [FromBody] ClienteEditarRequest request)
        {
            await _clienteService.AtualizarCliente(id, request);
            return NoContent();
        }
    }
}
