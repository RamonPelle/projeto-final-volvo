namespace TechStore.Models.Enums
{
    /// <summary>
    /// Status possível de um pedido na aplicação.
    /// </summary>
    public enum StatusPedido
    {
        /// <summary>
        /// Pedido criado, mas ainda não finalizado/concluído.
        /// </summary>
        Pendente = 1,

        /// <summary>
        /// Pedido já finalizado/concluído.
        /// </summary>
        Concluido = 2,
    }
}
