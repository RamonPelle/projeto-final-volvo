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
        /// Lista de itens do pedido.
        /// </summary>
        public List<ItensResponse> Itens { get; set; } = new();

        /// <summary>
        /// DTO para a resposta de um item de pedido.
        /// </summary>
        public class ItensResponse
        {
            /// <summary>
            /// ID do item do pedido.
            /// </summary>
            /// <example>1</example>
            public int Id { get; set; }

            /// <summary>
            /// Quantidade do produto.
            /// </summary>
            /// <example>2</example>
            public int Quantidade { get; set; }

            /// <summary>
            /// Preço unitário do produto no momento da compra.
            /// </summary>
            /// <example>4500.50</example>
            public decimal PrecoUnitario { get; set; }
        }
    }
}
