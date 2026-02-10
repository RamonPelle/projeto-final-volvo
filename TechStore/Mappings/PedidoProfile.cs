using AutoMapper;
using TechStore.Models;
using TechStore.DTOs.Response;

namespace TechStore.Mappings
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<Pedido, PedidoResponse>()
                .ForMember(
                    dest => dest.Itens,
                    opt => opt.MapFrom(src => src.Itens)
                );

            CreateMap<ItemPedido, PedidoResponse.ItensResponse>();
        }
    }
}
