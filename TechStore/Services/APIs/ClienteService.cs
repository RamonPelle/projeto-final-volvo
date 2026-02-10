using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.Models.DTOs.Request;
using TechStore.Repository.api;
using AutoMapper;
using TechStore.Utils;

namespace TechStore.Services.api
{
    public class ClienteService
    {
        private readonly IMapper _mapper;

        private readonly ClienteRepository _clienteRepository;
        private readonly SenhaService _SenhaService;

        public ClienteService(ClienteRepository clienteRepository, SenhaService SenhaService, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _SenhaService = SenhaService;
            _mapper = mapper;

            _clienteRepository = clienteRepository;
            _SenhaService = SenhaService;
            _mapper = mapper;
        }

        public async Task<Cliente> AdicionarCliente(ClienteRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Dados do cliente não podem ser nulos.");

            var clienteExistente = await _clienteRepository.BuscarClientePorEmail(request.Email);

            if (clienteExistente != null)
            {
                throw new ValidationException("Este e-mail já está em uso.");
            }

            var cliente = _mapper.Map<Cliente>(request);
            cliente.PasswordHash = _SenhaService.EncriptaSenha(request.Senha);

            var erros = ValidadorEntidade.Validar(cliente);
            if (erros.Any())
            {
                throw new ValidationException(string.Join("; ", erros));
            }

            await _clienteRepository.AdicionarCliente(cliente);
            return cliente;
        }

        public async Task<List<Cliente>> ObterTodosClientes()
        {
            return await _clienteRepository.BuscarTodosClientes();
        }

        public async Task<Cliente?> BuscarClientePorId(int id)
        {
            return await _clienteRepository.BuscarClientePorId(id);
        }

        public async Task DeletarCliente(int id)
        {

            var cliente = await _clienteRepository.BuscarClientePorId(id);

            if (cliente == null)
                throw new KeyNotFoundException($"Cliente com id {id} não encontrado.");

            await _clienteRepository.DeletarCliente(id);
        }

        public async Task AtualizarCliente(int id, ClienteEditarRequest clienteUpdate)
        {
            if (clienteUpdate == null)
                throw new ArgumentNullException(nameof(clienteUpdate), "Dados de atualização do cliente não podem ser nulos.");

            var clienteExistente = await _clienteRepository.BuscarClientePorId(id);

            if (clienteExistente == null)
                throw new KeyNotFoundException($"Cliente com id {id} não encontrado.");

            var clienteComNovoEmail = await _clienteRepository.BuscarClientePorEmail(clienteUpdate.Email);

            if (clienteComNovoEmail != null && clienteComNovoEmail.Id != id)
            {
                throw new ValidationException("Este e-mail já está em uso por outra conta.");
            }

            _mapper.Map(clienteUpdate, clienteExistente);

            var erros = ValidadorEntidade.Validar(clienteExistente);
            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _clienteRepository.AtualizarCliente(clienteExistente);
        }
    }
}