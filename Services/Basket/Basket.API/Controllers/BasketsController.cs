using System.Net;
using Basket.Application.Baskets.Commands.CreateShoppingCard;
using Basket.Application.Baskets.Commands.DeleteShoppingCart;
using Basket.Application.Baskets.Queries.GetBasketByUserName;
using Basket.Application.Baskets.Requests;
using Basket.Application.Baskets.Response;
using Basket.Application.Common.Models;
using Basket.Application.GrpcService;
using Basket.Application.Mappings;
using EventBus.Service.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

public class BasketsController : ApiControllerBase
{
    private readonly DiscountGrpcService _grpcService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<BasketsController> _logger;

    public BasketsController(DiscountGrpcService grpcService, IPublishEndpoint publishEndpoint, ILogger<BasketsController> logger)
    {
        _grpcService = grpcService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
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
    
    [HttpPost("Checkout")]
    [ProducesResponseType((int) HttpStatusCode.Accepted)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CheckoutAsync([FromBody] BasketCheckoutPlacedRequest request)
    {
        //Get existing basket with username
        var basket = await Mediator.Send(new GetBasketByUserNameQuery(request.UserName));
        if (basket.Data == null)
        {
            return BadRequest();
        }

        // TODO: this code should be handled in application level
        var eventMessage = BasketMapper.Mapper.Map<BasketCheckoutPlacedEvent>(request);
        eventMessage.TotalPrice = basket.Data.TotalPrice;
        // eventMesg.CorrelationId = _correlationIdGenerator.Get();
        await _publishEndpoint.Publish(eventMessage);
        //remove the basket
        var deleteQuery = new DeleteShoppingCartCommand(request.UserName);
        await Mediator.Send(deleteQuery);
        return Accepted();
    }
}