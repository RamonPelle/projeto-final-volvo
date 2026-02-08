using System.ComponentModel.DataAnnotations;
using TechStore.Models.Enums;

namespace TechStore.DTOs
{
    public class PedidoEditarDTO
    {
        [Required(ErrorMessage = "O status do pedido é obrigatório.")]
        public StatusPedido Status { get; set; }
    }
}