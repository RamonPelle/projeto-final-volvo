using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Request
{
    /// <summary>
    /// DTO para atualização parcial de preço de um produto.
    /// </summary>
    public class ProdutoAtualizarPrecoRequest
    {
        /// <summary>
        /// Novo preço do produto.
        /// </summary>
        /// <example>4999.90</example>
        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(
            typeof(decimal),
            "0.00",
            "9999999999999999.99",
            ErrorMessage = "O preço não pode ser negativo, nem possuir mais do que 16 dígitos."
        )]
        public decimal Preco { get; set; }
    }
}
