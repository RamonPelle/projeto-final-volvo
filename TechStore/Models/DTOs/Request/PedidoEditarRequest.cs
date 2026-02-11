using System.ComponentModel.DataAnnotations;
using TechStore.Models.Enums;

namespace TechStore.Models.DTOs.Request
{
    public class PedidoEditarRequest
    {
        [Required(ErrorMessage = "O status do pedido é obrigatório.")]
        public StatusPedido Status { get; set; }
    }
}