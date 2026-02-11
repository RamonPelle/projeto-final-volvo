using AutoMapper;
using TechStore.DTOs.Request;
using TechStore.Models;
using TechStore.Models.DTOs.Response;
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
