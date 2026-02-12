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

        /// <summary>
        /// ID do cliente que realizou o pedido.
        /// </summary>
        /// <example>1</example>
        public int ClienteId { get; set; }

        /// <summary>
        /// Data e hora em que o pedido foi criado.
        /// </summary>
        /// <example>2026-02-11T15:30:00</example>
        public DateTime Data { get; set; }

        /// <summary>
        /// Status atual do pedido.
        /// </summary>
        /// <example>Pendente</example>
        public StatusPedido Status { get; set; }
		
        /// <summary>
        /// Itens que compõem o pedido.
        /// </summary>
        public List<ItemPedidoResponse> Itens { get; set; } = new();
    }
}