using AutoMapper;
using EventBus.Service.Events;
using Ordering.Application.Order.Commands;
using Ordering.Application.Order.Responses;

namespace Ordering.Application.Common.Mapping;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Domain.Entities.Order, OrderResponse>().ReverseMap();
        CreateMap<Domain.Entities.Order, CreateCheckoutOrderCommand>().ReverseMap();
        CreateMap<Domain.Entities.Order, UpdateOrderCommand>().ReverseMap();
        CreateMap<CreateCheckoutOrderCommand, BasketCheckoutPlacedEvent>().ReverseMap();
    }
}