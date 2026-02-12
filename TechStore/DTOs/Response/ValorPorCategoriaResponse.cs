using System.ComponentModel.DataAnnotations;

namespace TechStore.DTOs.Response
{
    /// <summary>
    /// DTO de saída para representar o valor total de vendas por categoria.
    /// </summary>
    public class ValorPorCategoriaResponse
    {
        /// <summary>
        /// Nome da categoria de produtos.
        /// </summary>
        /// <example>Notebooks</example>
        [Required]
        public string Categoria { get; set; } = null!;

        /// <summary>
        /// Valor total das vendas desta categoria.
        /// </summary>
        /// <example>15000.50</example>
        [Required]
        public decimal ValorTotal { get; set; }
    }
}
