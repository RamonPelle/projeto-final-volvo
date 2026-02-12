namespace TechStore.DTOs.Response
{
    /// <summary>
    /// DTO de saída com os dados públicos de um cliente.
    /// </summary>
    public class ClienteResponse
    {
        /// <summary>
        /// ID único do cliente.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome completo do cliente.
        /// </summary>
        /// <example>João da Silva</example>
        public string Nome { get; set; } = null!;

        /// <summary>
        /// E-mail do cliente.
        /// </summary>
        /// <example>joao.silva@example.com</example>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Telefone de contato do cliente.
        /// </summary>
        /// <example>+55 11 91234-5678</example>
        public string Telefone { get; set; } = null!;

    }
}