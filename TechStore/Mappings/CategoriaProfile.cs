using AutoMapper;
using TechStore.DTOs.Request;
using TechStore.Models;

namespace TechStore.Mappings
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CategoriaRequest, Categoria>();
        }
    }
}
