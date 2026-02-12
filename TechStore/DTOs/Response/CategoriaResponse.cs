namespace TechStore.DTOs.Response
{
    /// <summary>
    /// DTO de saída com os dados de uma categoria de produto.
    /// </summary>
    public class CategoriaResponse
    {
        /// <summary>
        /// ID único da categoria.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome da categoria.
        /// </summary>
        /// <example>Notebooks</example>
        public string Nome { get; set; } = null!;
    }
}
