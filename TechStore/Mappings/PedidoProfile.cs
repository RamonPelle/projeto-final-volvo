using AutoMapper;
using TechStore.Models;
using TechStore.DTOs.Request;
using TechStore.DTOs.Response;

namespace TechStore.Mappings
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<Pedido, PedidoResponse>();
            CreateMap<ItemPedido, ItemPedidoResponse>()
                .ForMember(response => response.NomeProduto, opt => opt.MapFrom(itemPedido => itemPedido.Produto.Nome));
            CreateMap<PedidoRequest, Pedido>();
            CreateMap<ItemPedidoRequest, ItemPedido>();
        }
    }
}
