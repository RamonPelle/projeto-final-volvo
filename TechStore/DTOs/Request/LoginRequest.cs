using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Senha { get; set; } = null!;
    }
}
