namespace TechStore.DTOs
{
    public class PedidoDTO
    {
        public int ClienteId { get; set; }
        public List<ItemPedidoDTO> Itens { get; set; } = new();
    }
}
