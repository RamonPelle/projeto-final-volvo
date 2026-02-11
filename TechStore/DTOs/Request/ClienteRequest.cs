using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    public class ClienteRequest
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Senha { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Telefone { get; set; } = null!;
    }
}
