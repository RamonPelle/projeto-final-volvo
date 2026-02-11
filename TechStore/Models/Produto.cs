using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models
{

    public partial class Produto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = null!;

        [Required]
        public int Estoque { get; set; }

        public int CategoriaId { get; set; }
    }
}
