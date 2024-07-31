using Basket.Application.Baskets.Response;
using Basket.Application.Common.Interfaces;
using Basket.Application.Common.Models;
using Basket.Application.Mappings;
using MediatR;

namespace Basket.Application.Baskets.Queries.GetBasketByUserName;

public class GetBasketByUserNameQuery : IRequest<ApiResponse<ShoppingCartResponse>>
{
    public string UserName { get; private set; }

    public GetBasketByUserNameQuery(string userName) => UserName = userName;
}

public class
    GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ApiResponse<ShoppingCartResponse>>
{
    private readonly IBasketService _service;

    public GetBasketByUserNameQueryHandler(IBasketService service)
    {
        _service = service;
    }

    public async Task<ApiResponse<ShoppingCartResponse>> Handle(GetBasketByUserNameQuery request,
        CancellationToken cancellationToken)
    {
        var basket = await _service.GetBasket(request.UserName);

        return ApiResponse.Ok(BasketMapper.Mapper.Map<ShoppingCartResponse>(basket));
    }
}