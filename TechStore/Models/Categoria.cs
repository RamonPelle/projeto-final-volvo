using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TechStore.Models;
namespace TechStore.Models
{

    public partial class Categoria
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nome { get; set; } = null!;
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}