using AutoMapper;
using TechStore.Models;
using TechStore.Models.DTOs.Response;

namespace TechStore.Mappings
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, CategoriaResponse>();
            CreateMap<Produto, ProdutoResponse>();
        }
    }
}
