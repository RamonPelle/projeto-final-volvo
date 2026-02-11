namespace TechStore.Models.DTOs.Request
{
    public class PedidoRequest
    {
        public int ClienteId { get; set; }
        public List<ItemPedidoRequest> Itens { get; set; } = new();
    }
}
