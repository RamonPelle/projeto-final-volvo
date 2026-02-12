using System.ComponentModel.DataAnnotations;
using TechStore.Models;
using TechStore.DTOs.Request;
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

        public async Task<Cliente> AdicionarCliente(ClienteRequest clienteRequest)
        {
            if (clienteRequest == null)
                throw new ArgumentNullException(nameof(clienteRequest), "Dados do cliente não podem ser nulos.");

            var clienteExistente = await _clienteRepository.BuscarClientePorEmail(clienteRequest.Email);

            if (clienteExistente != null)
            {
                throw new ValidationException("Este e-mail já está em uso.");
            }

            var cliente = _mapper.Map<Cliente>(clienteRequest);
            cliente.SenhaEncriptada = _SenhaService.EncriptaSenha(clienteRequest.Senha);

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

        public async Task AtualizarCliente(int id, ClientePutRequest clienteUpdate)
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

        public async Task AtualizarClienteParcial(int id, ClientePatchRequest clientePatch)
        {
            if (clientePatch == null)
                throw new ArgumentNullException(nameof(clientePatch), "Dados de atualização do cliente não podem ser nulos.");

            var clienteExistente = await _clienteRepository.BuscarClientePorId(id);

            if (clienteExistente == null)
                throw new KeyNotFoundException($"Cliente com id {id} não encontrado.");

            if (clientePatch.Nome == null && clientePatch.Email == null && clientePatch.Telefone == null)
                throw new ValidationException("Pelo menos um campo deve ser informado para atualização.");

            if (!string.IsNullOrWhiteSpace(clientePatch.Email) && clientePatch.Email != clienteExistente.Email)
            {
                var clienteComNovoEmail = await _clienteRepository.BuscarClientePorEmail(clientePatch.Email);

                if (clienteComNovoEmail != null && clienteComNovoEmail.Id != id)
                {
                    throw new ValidationException("Este e-mail já está em uso por outra conta.");
                }

                clienteExistente.Email = clientePatch.Email;
            }

            if (!string.IsNullOrWhiteSpace(clientePatch.Nome))
            {
                clienteExistente.Nome = clientePatch.Nome;
            }

            if (!string.IsNullOrWhiteSpace(clientePatch.Telefone))
            {
                clienteExistente.Telefone = clientePatch.Telefone;
            }

            var erros = ValidadorEntidade.Validar(clienteExistente);
            if (erros.Any())
                throw new ValidationException(string.Join("; ", erros));

            await _clienteRepository.AtualizarCliente(clienteExistente);
        }
    }
}