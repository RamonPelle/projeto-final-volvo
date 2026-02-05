using System.ComponentModel.DataAnnotations;
using TechStore.Models;
namespace TechStore.DTOs
{
    public class CategoriaDTO
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        public string Nome { get; set; } = null!;
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
