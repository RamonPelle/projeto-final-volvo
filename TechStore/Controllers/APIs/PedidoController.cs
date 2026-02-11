using Microsoft.AspNetCore.Mvc;
using TechStore.Services.api;
using TechStore.Models.DTOs.Request;
using TechStore.Models.DTOs.Response;
using TechStore.Models.Enums;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;

namespace TechStore.Controllers.api
{
    /// <summary>
    /// Controller para operações de Pedido.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
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
        [SwaggerOperation(Summary = "Retorna pedidos por cliente e status", Description = "Obtém a lista de pedidos para um cliente específico, opcionalmente filtrada por status.")]
        [SwaggerResponse(200, "Lista de pedidos retornada com sucesso.", typeof(List<PedidoResponse>))]
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
        [SwaggerOperation(Summary = "Retorna pedido por ID", Description = "Obtém um pedido específico pelo seu ID.")]
        [SwaggerResponse(200, "Pedido encontrado.", typeof(PedidoResponse))]
        [SwaggerResponse(404, "Pedido não encontrado.")]
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
        [SwaggerOperation(Summary = "Cria novo pedido", Description = "Adiciona um novo pedido ao sistema.")]
        [SwaggerResponse(201, "Pedido criado com sucesso.", typeof(PedidoResponse))]
        [SwaggerResponse(400, "Erro de validação.")]
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
        [SwaggerOperation(Summary = "Edita pedido", Description = "Edita um pedido pelo seu ID.")]
        [SwaggerResponse(204, "Pedido editado com sucesso.")]
        [SwaggerResponse(404, "Pedido não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> FinalizarPedido(int id)
        {
            await _pedidoService.FinalizarPedido(id);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deleta pedido por ID", Description = "Remove um pedido específico pelo seu ID.")]
        [SwaggerResponse(204, "Pedido deletado com sucesso.")]
        [SwaggerResponse(404, "Pedido não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<ActionResult> DeletarPedido(int id)
        {
            await _pedidoService.DeletarPedido(id);
            return NoContent();
        }

        [HttpDelete("{id:int}/item/{itemId:int}")]
        [SwaggerOperation(Summary = "Deleta item de um pedido", Description = "Remove um item específico de um pedido.")]
        [SwaggerResponse(204, "Item do pedido deletado com sucesso.")]
        [SwaggerResponse(404, "Pedido ou item não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> DeletarItem(int id, int itemId)
        {
            await _itemPedidoService.DeletarItem(id, itemId);
            return NoContent();
        }
    }
}
