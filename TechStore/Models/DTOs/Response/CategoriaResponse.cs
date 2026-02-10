namespace TechStore.Models.DTOs.Response
{
    public class CategoriaResponse
    {
        public int Id { get; set; }
        public List<ProdutoResponse> Produtos { get; set; } = new();

    }
}
