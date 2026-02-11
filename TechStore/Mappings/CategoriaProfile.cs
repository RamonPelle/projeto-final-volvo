using AutoMapper;
using TechStore.Models;
using TechStore.DTOs.Response;
using TechStore.DTOs.Request;

namespace TechStore.Mappings
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            // GET
            CreateMap<Categoria, CategoriaResponse>();
            CreateMap<Produto, ProdutoResponse>();

            // POST / PUT
            CreateMap<CategoriaRequest, Categoria>();
        }
    }
}
