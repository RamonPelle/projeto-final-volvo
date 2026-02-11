using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    /// <summary>
    /// DTO para criar ou editar uma categoria.
    /// </summary>
    public class CategoriaRequest
    {
        /// <summary>
        /// Nome da categoria.
        /// </summary>
        /// <example>Notebooks</example>
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [MaxLength(30)]
        public string Nome { get; set; } = null!;
    }
}
