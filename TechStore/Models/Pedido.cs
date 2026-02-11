using System.ComponentModel.DataAnnotations;
using TechStore.Models.Enums;

namespace TechStore.Models
{
    /// <summary>
    /// Representa um pedido realizado por um cliente.
    /// </summary>
    public partial class Pedido
    {
        /// <summary>
        /// Identificador único do pedido.
        /// </summary>
        /// <example>101</example>
        public int Id { get; set; }

        /// <summary>
        /// Data e hora em que o pedido foi realizado.
        /// </summary>
        [Required]
        public DateTime Data { get; set; }

        /// <summary>
        /// ID do cliente que realizou o pedido.
        /// </summary>
        /// <example>1</example>
        [Required]
        public int ClienteId { get; set; }

        /// <summary>
        /// Coleção de itens que compõem o pedido.
        /// </summary>
        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        /// <summary>
        /// Status atual do pedido.
        /// </summary>
        /// <example>1</example>
        [Required]
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
    }
}
