using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Repository.api;
using TechStore.DTOs;
using TechStore.Utils;
using TechStore.Models.Enums;

namespace TechStore.Services.api
{
    public class PedidoService
    {
        private readonly PedidoRepository _pedidoRepository;

        public PedidoService(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository
                ?? throw new ArgumentNullException(nameof(pedidoRepository));
        }

        public async Task<List<Pedido>> ObterPedidosPorCliente(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("Id do cliente inválido.");

            return await _pedidoRepository.BuscarPorClienteId(clienteId);
        }

        public async Task<Pedido?> BuscarPedidoPorId(int id)
        {
            return await _pedidoRepository.BuscarPorId(id);
        }

        public async Task<Pedido> AdicionarPedido(int clienteId, PedidoDTO pedidoDto)
        {
            if (pedidoDto == null)
                throw new ArgumentNullException(nameof(pedidoDto), "O pedido não pode ser nulo.");

            var pedido = new Pedido
            {
                ClienteId = clienteId,
                Data = DateTime.UtcNow
            };

            var erros = ValidadorEntidade.Validar(pedido);

            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _pedidoRepository.AdicionarPedido(pedido);
            return pedido;
        }

        public async Task<StatusPedido> ObterStatus(int id)
        {
            var pedido = await _pedidoRepository.BuscarPorId(id);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            return pedido.Status;
        }

        public async Task DeletarPedido(int id)
        {
            var pedido = await _pedidoRepository.BuscarPorId(id);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            if (pedido.Status == StatusPedido.Concluido)
                throw new ArgumentException("Pedidos concluídos não podem ser excluídos.");

            await _pedidoRepository.DeletarPedido(id);
        }

        public async Task EditarPedido(int id, PedidoEditarDTO PedidoEditarDto)
        {
            if (id <= 0)
                throw new ArgumentException("Id do pedido inválido.");

            if (PedidoEditarDto == null)
                throw new ArgumentNullException(nameof(PedidoEditarDto));

            var pedido = await _pedidoRepository.BuscarPorId(id);

            if (pedido == null)
                throw new KeyNotFoundException("Pedido não encontrado.");

            if (pedido.Status == StatusPedido.Concluido)
                throw new ArgumentException("Pedidos concluídos não podem ser alterados.");

            pedido.Status = PedidoEditarDto.Status;

            await _pedidoRepository.EditarPedido(pedido);
        }
    }
}
