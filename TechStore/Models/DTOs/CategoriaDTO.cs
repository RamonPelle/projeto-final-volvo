using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs
{
    public class CategoriaDTO
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [MaxLength(30)]
        public string Nome { get; set; } = null!;
    }
}
