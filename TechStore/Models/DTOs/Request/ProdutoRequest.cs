using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    public class ProdutoRequest
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.00, 999999.99, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(400, ErrorMessage = "A descrição pode ter no máximo 400 caracteres.")]
        public string Descricao { get; set; } = null!;

        [Required(ErrorMessage = "O estoque é obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
        public int Estoque { get; set; }

        //TODO trocar pelo nome da categoria?
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; set; }
    }
}
