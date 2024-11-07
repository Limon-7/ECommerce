using AutoMapper;
using Basket.Application.Baskets.Requests;
using Basket.Application.Baskets.Response;
using Basket.Domain.Entities;
using EventBus.Service.Events;


namespace Basket.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();
        CreateMap<BasketCheckoutPlacedRequest, BasketCheckoutPlacedEvent>().ReverseMap();
        // CreateMap<BasketCheckoutV2, BasketCheckoutEventV2>().ReverseMap();
    }
}