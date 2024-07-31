using AutoMapper;
using Basket.Application.Baskets.Response;
using Basket.Domain.Entities;


namespace Basket.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();
        // CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        // CreateMap<BasketCheckoutV2, BasketCheckoutEventV2>().ReverseMap();
    }
}