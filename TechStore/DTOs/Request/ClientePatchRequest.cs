using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    /// <summary>
    /// DTO para atualização parcial dos dados de um cliente.
    /// </summary>
    public class ClientePatchRequest
    {
        /// <summary>
        /// Nome do cliente.
        /// </summary>
        /// <example>João da Silva</example>
        [MaxLength(100)]
        public string? Nome { get; set; }

        /// <summary>
        /// E-mail do cliente.
        /// </summary>
        /// <example>joao.silva@example.com</example>
        [EmailAddress]
        [MaxLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// Telefone do cliente.
        /// </summary>
        /// <example>+55 11 91234-5678</example>
        [MaxLength(20)]
        public string? Telefone { get; set; }
    }
}
