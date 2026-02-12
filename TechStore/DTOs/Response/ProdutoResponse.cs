namespace TechStore.DTOs.Response
{
    /// <summary>
    /// DTO de saída com os dados de um produto.
    /// </summary>
    public class ProdutoResponse
    {
        /// <summary>
        /// ID único do produto.
        /// </summary>
        /// <example>10</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        /// <example>Smartphone Top de Linha</example>
        public string Nome { get; set; } = null!;

        /// <summary>
        /// Descrição detalhada do produto.
        /// </summary>
        /// <example>Smartphone com 256GB de armazenamento, 12GB de RAM e câmera de 108MP.</example>
        public string Descricao { get; set; } = null!;

        /// <summary>
        /// Preço atual de venda do produto.
        /// </summary>
        /// <example>3999.90</example>
        public decimal Preco { get; set; }

        /// <summary>
        /// Quantidade disponível em estoque.
        /// </summary>
        /// <example>150</example>
        public int Estoque { get; set; }

        /// <summary>
        /// ID da categoria à qual o produto pertence.
        /// </summary>
        /// <example>1</example>
        public int CategoriaId { get; set; }
    }

}
