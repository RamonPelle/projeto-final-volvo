namespace TechStore.DTOs
{
    public class PedidoResponse
    {
        public int Id;
        public List<ItensResponse> Itens { get; set; } = new();

        public class ItensResponse
        {
            public int Id { get; set; }
            public int Quantidade { get; set; }
            public decimal PrecoUnitario { get; set; }
        }
    }
}
