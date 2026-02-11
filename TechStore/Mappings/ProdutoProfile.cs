using AutoMapper;
using TechStore.Models;
using TechStore.DTOs.Response;
using TechStore.DTOs.Request;
namespace TechStore.Mappings
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ProdutoRequest, Produto>();
            CreateMap<Produto, ProdutoResponse>();
        }
    }
}
