using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    /// <summary>
    /// DTO para requisição de login/autenticação de cliente.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// E-mail utilizado para autenticação.
        /// </summary>
        /// <example>joao.silva@example.com</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Senha em texto plano que será validada/encriptada no backend.
        /// </summary>
        /// <example>minhaSenhaForte123</example>
        [Required]
        public string Senha { get; set; } = null!;
    }
}
