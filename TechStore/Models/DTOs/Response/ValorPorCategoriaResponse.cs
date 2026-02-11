using System.ComponentModel.DataAnnotations;

namespace TechStore.Models.DTOs.Response
{
    public class ValorPorCategoriaResponse
    {
        [Required]
        public string Categoria { get; set; } = null!;

        [Required]
        public decimal ValorTotal { get; set; }
    }
}
