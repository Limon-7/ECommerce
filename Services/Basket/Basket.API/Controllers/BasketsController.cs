using System.Net;
using Basket.Application.Baskets.Commands.CreateShoppingCard;
using Basket.Application.Baskets.Commands.DeleteShoppingCart;
using Basket.Application.Baskets.Queries.GetBasketByUserName;
using Basket.Application.Baskets.Response;
using Basket.Application.Common.Models;
using Basket.Application.GrpcService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

public class BasketsController : ApiControllerBase
{
    private readonly DiscountGrpcService _grpcService;

    public BasketsController(DiscountGrpcService grpcService)
    {
        _grpcService = grpcService;
    }
    [HttpGet]
    [Route("[action]/{userName}", Name = "GetBasketByUserName")]
    [ProducesResponseType(typeof(ApiResponse<ShoppingCartResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ApiResponse<ShoppingCartResponse>>> GetBasket(string userName)
    {
        return Ok(await Mediator.Send(new GetBasketByUserNameQuery(userName)));
    }

    [HttpPost("CreateBasket")]
    [ProducesResponseType(typeof(ApiResponse<ShoppingCartResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ApiResponse<ShoppingCartResponse>>> UpdateBasket(
        [FromBody] CreateShoppingCartCommand createShoppingCartCommand)
    {
        foreach (var item in createShoppingCartCommand.Items)
        {
            var coupon = await _grpcService.GetDiscount(item.ProductId);

            item.Price -= coupon.Amount;
        }
        return Ok(await Mediator.Send(createShoppingCartCommand));
    }

    [HttpDelete]
    [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
    [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ApiResponse<Unit>>> DeleteBasket(string userName)
    {
        return Ok(await Mediator.Send(new DeleteShoppingCartCommand(userName)));
    }
}