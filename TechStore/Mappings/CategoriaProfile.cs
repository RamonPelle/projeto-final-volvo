using AutoMapper;
using TechStore.Models;
using TechStore.Models.DTOs.Response;
using TechStore.Models.DTOs.Request;

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
