using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TechStore.Models;
namespace TechStore.Models
{

    /// <summary>
    /// Representa uma categoria de produtos.
    /// </summary>
    public partial class Categoria
    {
        /// <summary>
        /// ID único da categoria.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome da categoria.
        /// </summary>
        /// <example>Smartphones</example>
        [Required]
        [MaxLength(30)]
        public string Nome { get; set; } = null!;

        /// <summary>
        /// Lista de produtos associados a esta categoria.
        /// </summary>
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}