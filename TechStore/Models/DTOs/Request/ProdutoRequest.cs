using System.ComponentModel.DataAnnotations;

namespace TechStore.Models.DTOs.Request
{
    public class ProdutoRequest
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(
            typeof(decimal),
            "0.00",
            "9999999999999999.99",
            ErrorMessage = "O preço não pode ser negativo, nem possuir mais do que 16 dígitos."
        )]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(400, ErrorMessage = "A descrição pode ter no máximo 400 caracteres.")]
        public string Descricao { get; set; } = null!;

        [Required(ErrorMessage = "O estoque é obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; set; }
    }
}
