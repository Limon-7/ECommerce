using Basket.Application.Baskets.Response;
using Basket.Application.Common.Interfaces;
using Basket.Application.Common.Models;
using MediatR;

namespace Basket.Application.Baskets.Commands.DeleteShoppingCart;

public class DeleteShoppingCartCommand : IRequest<ApiResponse<Unit>>
{
    public string UserName { get; private set; }

    public DeleteShoppingCartCommand(string userName) => UserName = userName;
}

public class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand, ApiResponse<Unit>>
{
    private readonly IBasketService _service;

    public DeleteShoppingCartCommandHandler(IBasketService service)
    {
        _service = service;
    }

    public async Task<ApiResponse<Unit>> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _service.DeleteBasket(request.UserName);
            return ApiResponse.Ok(Unit.Value);
        }
        catch (Exception e)
        {
            return ApiResponse.Fail<Unit>(e.Message);
        }
    }
}