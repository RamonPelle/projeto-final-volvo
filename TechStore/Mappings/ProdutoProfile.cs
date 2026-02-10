using AutoMapper;
using TechStore.DTOs.Request;
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
