using System.ComponentModel.DataAnnotations;
using TechStore.Models.Enums;

namespace TechStore.DTOs.Request
{
    /// <summary>
    /// DTO para editar o status de um pedido existente.
    /// </summary>
    public class PedidoEditarRequest
    {
        /// <summary>
        /// Novo status do pedido.
        /// </summary>
        /// <example>2</example>
        [Required(ErrorMessage = "O status do pedido é obrigatório.")]
        public StatusPedido Status { get; set; }
    }
}