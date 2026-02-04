using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TechStore.Models;
namespace TechStore.Models;

public partial class ItemPedido
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int Quantidade { get; set; }

    [Column(TypeName = "decimal(8,2)")] // Define max 999.999,99
    [Required]
    public decimal PrecoUnitario { get; set; }

    public int PedidoId { get; set; }

    [ForeignKey("PedidoId")]
    public Pedido pedido { get; set; } = null!;

    public int ProdutoId { get; set; }

    [ForeignKey("ProdutoId")]
    public Produto produto { get; set; } = null!;

}
