using System.Net;
using Basket.Application.Baskets.Commands.CreateShoppingCard;
using Basket.Application.Baskets.Commands.DeleteShoppingCart;
using Basket.Application.Baskets.Queries.GetBasketByUserName;
using Basket.Application.Baskets.Response;
using Basket.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

public class BasketsController : ApiControllerBase
{
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