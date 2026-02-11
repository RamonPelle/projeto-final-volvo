using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models
{

        /// <summary>
        /// Representa um item dentro de um pedido.
        /// </summary>
        public partial class ItemPedido
        {
                /// <summary>
                /// Identificador único do item do pedido.
                /// </summary>
                /// <example>1</example>
                public int Id { get; set; }

                /// <summary>
                /// Quantidade do produto neste item.
                /// </summary>
                /// <example>2</example>
                [Required]
                public int Quantidade { get; set; }

                /// <summary>
                /// Preço unitário do produto no momento da compra.
                /// </summary>
                /// <example>4500.50</example>
                [Column(TypeName = "decimal(18,2)")]
                [Required]
                public decimal PrecoUnitario { get; set; }

                /// <summary>
                /// ID do pedido ao qual este item pertence.
                /// </summary>
                /// <example>101</example>
                public int PedidoId { get; set; }
                public Pedido Pedido { get; set; } = null!;

                /// <summary>
                /// ID do produto associado a este item.
                /// </summary>
                /// <example>205</example>
                public int ProdutoId { get; set; }
                public Produto Produto { get; set; } = null!;
        }
}