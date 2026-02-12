using AutoMapper;
using TechStore.Models;
using TechStore.DTOs.Request;
using TechStore.DTOs.Response;

namespace TechStore.Mappings
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteRequest, Cliente>();
            CreateMap<ClientePutRequest, Cliente>();
        }
    }
}