using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    public class ClienteEditarRequest
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Telefone { get; set; } = null!;
    }
}
