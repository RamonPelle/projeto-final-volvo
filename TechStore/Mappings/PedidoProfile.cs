using AutoMapper;
using TechStore.Models;
using TechStore.DTOs.Response;

namespace TechStore.Mappings
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<Pedido, PedidoResponse>();
            CreateMap<ItemPedido, ItemPedidoResponse>();
        }
    }
}
