namespace TechStore.DTOs.Request
{
    /// <summary>
    /// DTO para criar um novo pedido.
    /// </summary>
    public class PedidoRequest
    {
        /// <summary>
        /// ID do cliente que está realizando o pedido.
        /// </summary>
        /// <example>1</example>
        public int ClienteId { get; set; }

        /// <summary>
        /// Lista de itens que compõem o pedido.
        /// </summary>
        public List<ItemPedidoRequest> Itens { get; set; } = new();
    }
}
