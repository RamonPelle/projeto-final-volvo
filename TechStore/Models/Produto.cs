using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechStore.Models;

namespace TechStore.Models
{

    /// <summary>
    /// Representa um produto disponível na loja.
    /// </summary>
    public partial class Produto
    {
        /// <summary>
        /// Identificador único do produto.
        /// </summary>
        /// <example>205</example>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        /// <example>Notebook Gamer XPTO</example>
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        /// <summary>
        /// Preço de venda do produto.
        /// </summary>
        /// <example>7999.99</example>
        [Column(TypeName = "decimal(8,2)")] // Define max 999.999,99
        public decimal Preco { get; set; }

        /// <summary>
        /// Descrição detalhada do produto.
        /// </summary>
        /// <example>Notebook com processador i9, 32GB RAM, SSD 1TB e placa de vídeo RTX 4090.</example>
        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; } = null!;

        /// <summary>
        /// Quantidade do produto disponível em estoque.
        /// </summary>
        /// <example>50</example>
        [Required]
        public int Estoque { get; set; }

        /// <summary>
        /// ID da categoria à qual o produto pertence.
        /// </summary>
        /// <example>1</example>
        public int CategoriaId { get; set; }
    }
}
