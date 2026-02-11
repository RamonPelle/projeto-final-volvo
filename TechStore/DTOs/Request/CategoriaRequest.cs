using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    public class CategoriaRequest
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [MaxLength(30)]
        public string Nome { get; set; } = null!;
    }
}
