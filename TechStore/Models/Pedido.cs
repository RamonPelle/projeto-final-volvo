using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TechStore.Models;
namespace TechStore.Models
{

    public partial class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }

    }
}
