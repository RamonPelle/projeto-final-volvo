using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    /// <summary>
    /// DTO para criação de um novo cliente.
    /// </summary>
    public class ClienteRequest
    {
        /// <summary>
        /// Nome completo do cliente.
        /// </summary>
        /// <example>João da Silva</example>
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        /// <summary>
        /// E-mail do cliente.
        /// </summary>
        /// <example>joao.silva@example.com</example>
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Senha em texto plano que será encriptada no backend.
        /// </summary>
        /// <example>minhaSenhaForte123</example>
        [Required]
        [MinLength(6)]
        public string Senha { get; set; } = null!;

        /// <summary>
        /// Telefone de contato do cliente.
        /// </summary>
        /// <example>+55 11 91234-5678</example>
        [Required]
        [MaxLength(20)]
        public string Telefone { get; set; } = null!;
    }
}
