using Microsoft.AspNetCore.Mvc;
using TechStore.Models;
using TechStore.Services.api;
using TechStore.DTOs.Request;
using TechStore.DTOs.Response;
using TechStore.Models.Enums;
using Swashbuckle.AspNetCore.Annotations;

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

        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna pedidos por cliente e status", Description = "Obtém a lista de pedidos para um cliente específico, opcionalmente filtrada por status.")]
        [SwaggerResponse(200, "Lista de pedidos retornada com sucesso.", typeof(List<PedidoResponse>))]
        public async Task<ActionResult<List<PedidoResponse>>> GetPedidosPorCliente([FromQuery] int clienteId, [FromQuery] StatusPedido status)
        {
            var pedidos = await _pedidoService.ObterPedidos(clienteId, status);

            var resposta = pedidos.Select(p => new PedidoResponse
            {
                Id = p.Id,
                Itens = p.Itens.Select(i => new PedidoResponse.ItensResponse
                {
                    Id = i.Id,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario
                }).ToList()
            });

            return Ok(resposta);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Retorna pedido por ID", Description = "Obtém um pedido específico pelo seu ID.")]
        [SwaggerResponse(200, "Pedido encontrado.", typeof(PedidoResponse))]
        [SwaggerResponse(404, "Pedido não encontrado.")]
        public async Task<ActionResult<PedidoResponse>> GetPedidoPorId(int id)
        {
            var pedido = await _pedidoService.BuscarPedidoPorId(id);

            if (pedido is null)
                return NotFound();

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

            return Ok(resposta);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria novo pedido", Description = "Adiciona um novo pedido ao sistema.")]
        [SwaggerResponse(201, "Pedido criado com sucesso.", typeof(PedidoResponse))]
        [SwaggerResponse(400, "Erro de validação.")]
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
        [SwaggerOperation(Summary = "Edita pedido", Description = "Edita um pedido pelo seu ID.")]
        [SwaggerResponse(204, "Pedido editado com sucesso.")]
        [SwaggerResponse(404, "Pedido não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
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
        [SwaggerOperation(Summary = "Deleta pedido por ID", Description = "Remove um pedido específico pelo seu ID.")]
        [SwaggerResponse(204, "Pedido deletado com sucesso.")]
        [SwaggerResponse(404, "Pedido não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
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

        [HttpDelete("{id:int}/item/{itemid:int}")]
        [SwaggerOperation(Summary = "Deleta item de um pedido", Description = "Remove um item específico de um pedido.")]
        [SwaggerResponse(204, "Item do pedido deletado com sucesso.")]
        [SwaggerResponse(404, "Pedido ou item não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> DeletarItem(int id, int itemId)
        {
            try
            {
                await _pedidoService.DeletarItem(id, itemId);
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

        [HttpGet("valorTotalVendidoPorCategoria")]
        public async Task<ActionResult<IEnumerable<ValorPorCategoriaResponse>>> GetValorTotalVendidoPorCategoria()
        {
            var resultado = await _pedidoService.ObterValorTotalVendidoPorCategoria();
            return Ok(resultado);
        }
    }
}
