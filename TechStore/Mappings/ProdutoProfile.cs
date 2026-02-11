using AutoMapper;
using TechStore.Models.DTOs.Request;
using TechStore.Models;

namespace TechStore.Mappings
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ProdutoRequest, Produto>();
        }
    }
}
