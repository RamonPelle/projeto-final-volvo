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
        [SwaggerOperation(Summary = "Retorna pedidos por cliente e status", Description = "Obtém a lista de pedidos para um cliente específico, opcionalmente filtrada por status. Regras de negócio: se um clienteId for informado, ele deve existir; caso contrário, é retornado erro de cliente não encontrado.")]
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
        [SwaggerOperation(Summary = "Retorna pedido por ID", Description = "Obtém um pedido específico pelo seu ID. Regras de negócio: o ID deve ser maior que zero; se o pedido não existir, é retornado erro de não encontrado.")]
        [SwaggerResponse(200, "Pedido encontrado.", typeof(PedidoResponse))]
        [SwaggerResponse(404, "Pedido não encontrado.")]
        public async Task<ActionResult<PedidoResponse>> BuscarPedidoPorId(int id)
        {
            var pedido = await _pedidoService.BuscarPedidoPorId(id);

            if (pedido is null)
                return NotFound();

            return Ok(_mapper.Map<PedidoResponse>(pedido));
        }

        [HttpGet("valorTotalPorCategoria")]
        [SwaggerOperation(Summary = "Retorna valor total vendido por categoria", Description = "Obtém o valor total vendido agrupado por categoria de produto.")]
        [SwaggerResponse(200, "Valores por categoria retornados com sucesso.", typeof(IEnumerable<ValorPorCategoriaResponse>))]
        public async Task<ActionResult<IEnumerable<ValorPorCategoriaResponse>>> ObterValorTotalVendidoPorCategoria()
        {
            var resultado = await _pedidoService.ObterValorTotalVendidoPorCategoria();
            return Ok(resultado);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria novo pedido", Description = "Adiciona um novo pedido ao sistema. Regras de negócio: o cliente não pode possuir outro pedido pendente; o pedido deve conter pelo menos um item; o cliente informado deve existir; cada item deve referenciar um produto existente, com quantidade maior que zero e que não exceda o estoque disponível.")]
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
        [SwaggerOperation(Summary = "Finaliza pedido", Description = "Finaliza um pedido pelo seu ID. Regras de negócio: o ID deve ser maior que zero; o pedido deve existir e não pode já estar concluído; ao finalizar, o estoque de cada produto é decrementado pela quantidade de itens.")]
        [SwaggerResponse(204, "Pedido finalizado com sucesso.")]
        [SwaggerResponse(404, "Pedido não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<IActionResult> FinalizarPedido(int id)
        {
            await _pedidoService.FinalizarPedido(id);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deleta pedido por ID", Description = "Remove um pedido específico pelo seu ID. Regras de negócio: o pedido deve existir e não pode estar com status Concluído; pedidos concluídos não podem ser excluídos.")]
        [SwaggerResponse(204, "Pedido deletado com sucesso.")]
        [SwaggerResponse(404, "Pedido não encontrado.")]
        [SwaggerResponse(400, "Erro de validação.")]
        public async Task<ActionResult> DeletarPedido(int id)
        {
            await _pedidoService.DeletarPedido(id);
            return NoContent();
        }

        [HttpDelete("{id:int}/item/{itemId:int}")]
        [SwaggerOperation(Summary = "Deleta item de um pedido", Description = "Remove um item específico de um pedido. Regras de negócio: o pedidoId e o itemId devem ser maiores que zero; o item deve existir e pertencer ao pedido informado; caso contrário, é retornado erro de validação.")]
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
