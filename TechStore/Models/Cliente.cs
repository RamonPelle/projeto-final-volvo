using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TechStore.Models;
namespace TechStore.Models
{

    /// <summary>
    /// Representa um cliente da loja.
    /// </summary>
    public partial class Cliente
    {
        /// <summary>
        /// Identificador único do cliente.
        /// </summary>
        /// <example>1</example>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Nome completo do cliente.
        /// </summary>
        /// <example>João da Silva</example>
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = null!;

    }
}