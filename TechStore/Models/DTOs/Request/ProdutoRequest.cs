using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    /// <summary>
    /// DTO para criar ou editar um produto.
    /// </summary>
    public class ProdutoRequest
    {
        /// <summary>
        /// Nome do produto.
        /// </summary>
        /// <example>Smartphone Top de Linha</example>
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = null!;

        /// <summary>
        /// Preço de venda do produto.
        /// </summary>
        /// <example>3999.90</example>
        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        /// <summary>
        /// Descrição detalhada do produto.
        /// </summary>
        /// <example>Smartphone com 256GB de armazenamento, 12GB de RAM e câmera de 108MP.</example>
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(400, ErrorMessage = "A descrição pode ter no máximo 400 caracteres.")]
        public string Descricao { get; set; } = null!;

        /// <summary>
        /// Quantidade do produto em estoque.
        /// </summary>
        /// <example>150</example>
        [Required(ErrorMessage = "O estoque é obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
        public int Estoque { get; set; }

        /// <summary>
        /// ID da categoria à qual o produto pertence.
        /// </summary>
        /// <example>1</example>
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        public int CategoriaId { get; set; }
    }
}
