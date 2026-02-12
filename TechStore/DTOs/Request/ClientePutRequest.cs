using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    /// <summary>
    /// DTO para atualização completa dos dados de um cliente (via PUT).
    /// </summary>
    public class ClientePutRequest
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
        /// Telefone de contato do cliente.
        /// </summary>
        /// <example>+55 11 91234-5678</example>
        [Required]
        [MaxLength(20)]
        public string Telefone { get; set; } = null!;
    }
}
