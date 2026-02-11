using TechStore.Models.Enums;

namespace TechStore.Models.DTOs.Response
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Data { get; set; }
        public StatusPedido Status { get; set; }
        public List<ItemPedidoResponse> Itens { get; set; } = new();
    }
}
