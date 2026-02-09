using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TechStore.Models.Enums;

namespace TechStore.Models
{
    public partial class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public int ClienteId { get; set; }

        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        [Required]
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
    }
}
