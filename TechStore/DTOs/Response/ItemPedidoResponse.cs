namespace TechStore.DTOs.Response
{
        /// <summary>
        /// DTO de saída representando um item dentro de um pedido.
        /// </summary>
        public class ItemPedidoResponse
        {
                /// <summary>
                /// ID do produto associado ao item do pedido.
                /// </summary>
                /// <example>205</example>
                public int Id { get; set; }
                public int ProdutoId { get; set; }

                /// <summary>
                /// Nome do produto no momento do pedido.
                /// </summary>
                /// <example>Notebook Gamer</example>
                public string NomeProduto { get; set; } = string.Empty;

                /// <summary>
                /// Quantidade do produto neste item do pedido.
                /// </summary>
                /// <example>2</example>
                public int Quantidade { get; set; }

                /// <summary>
                /// Preço unitário aplicado ao produto neste pedido.
                /// </summary>
                /// <example>5999.90</example>
                public decimal PrecoUnitario { get; set; }
        }

}