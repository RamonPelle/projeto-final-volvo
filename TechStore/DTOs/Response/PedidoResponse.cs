using TechStore.Models.Enums;

namespace TechStore.DTOs.Response
{
    /// <summary>
    /// DTO para a resposta de um pedido.
    /// </summary>
    public class PedidoResponse
    {
        /// <summary>
        /// ID do pedido.
        /// </summary>
        /// <example>101</example>
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Data { get; set; }
        public StatusPedido Status { get; set; }
        public List<ItemPedidoResponse> Itens { get; set; } = new();
    }
}