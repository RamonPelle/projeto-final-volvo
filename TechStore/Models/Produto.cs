using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TechStore.Models
{

    public partial class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        [Column(TypeName = "decimal(8,2)")] // Define max 999.999,99
        [DefaultValue(0)]
        public decimal Preco { get; set; }

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = null!;

        [Required]
        [DefaultValue(0)]
        public int Estoque { get; set; }

        public int CategoriaId { get; set; }
    }
}
