namespace TechStore.DTOs.Request
{
    /// <summary>
    /// DTO para representar um item dentro de uma requisição de criação de pedido.
    /// </summary>
    public class ItemPedidoRequest
    {
        /// <summary>
        /// ID do produto a ser adicionado ao pedido.
        /// </summary>
        /// <example>205</example>
        public int ProdutoId { get; set; }

        /// <summary>
        /// Quantidade do produto.
        /// </summary>
        /// <example>1</example>
        public int Quantidade { get; set; }
    }
}