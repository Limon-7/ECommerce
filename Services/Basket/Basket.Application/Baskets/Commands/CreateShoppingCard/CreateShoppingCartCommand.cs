using Basket.Application.Baskets.Response;
using Basket.Application.Common.Interfaces;
using Basket.Application.Common.Models;
using Basket.Application.Mappings;
using Basket.Domain.Entities;
using MediatR;

namespace Basket.Application.Baskets.Commands.CreateShoppingCard;

public class CreateShoppingCartCommand: IRequest<ApiResponse<ShoppingCartResponse>>
{
    public CreateShoppingCartCommand(string userName, List<ShoppingCartItem> items)
    {
        UserName = userName;
        Items = items;
    }
    public string UserName { get; private set; }
    public List<ShoppingCartItem> Items { get; private set; }
}

public class
    CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ApiResponse<ShoppingCartResponse>>
{
    private readonly IBasketService _service;

    public CreateShoppingCartCommandHandler(IBasketService service)
    {
        _service = service;
    }
    public async Task<ApiResponse<ShoppingCartResponse>> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var response = await _service.UpdateBasket(new ShoppingCart(request.UserName, request.Items));
        var data = BasketMapper.Mapper.Map<ShoppingCartResponse>(response);
        return ApiResponse.Ok(data);
    }
}