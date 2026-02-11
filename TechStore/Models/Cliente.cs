using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using TechStore.Models;
namespace TechStore.Models
{

    /// <summary>
    /// Representa um cliente da loja.
    /// </summary>
    public partial class Cliente
    {
        /// <summary>
        /// Identificador único do cliente.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome completo do cliente.
        /// </summary>
        /// <example>João da Silva</example>
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string SenhaEncriptada { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Telefone { get; set; } = null!;

    }
}