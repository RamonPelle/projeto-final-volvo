using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TechStore.Models;
namespace TechStore.Models;

public partial class Produto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Nome { get; set; } = null;

    public decimal Preco { get; set; }
    public string Descricao { get; set; } = null;

    public int CategoriaId { get; set; }

    [ForeignKey("CategoriaId")]
    public Categoria categoria { get; set; } = null;


}
