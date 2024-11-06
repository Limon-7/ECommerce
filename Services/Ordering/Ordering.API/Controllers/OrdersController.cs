using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Models;
using Ordering.Application.Order.Commands;
using Ordering.Application.Order.Queries;
using Ordering.Application.Order.Responses;

namespace Ordering.API.Controllers;

public class OrdersController : ApiControllerBase
{
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{userName}", Name = "GetOrdersByUserName")]
    [ProducesResponseType(typeof(List<OrderResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<OrderResponse>>> GetOrdersByUserName(string userName)
    {
        var orders = await Mediator.Send(new GetOrdersQuery(userName));
        return Ok(orders);
    }

    //Just for testing locally as it will be processed in queue
    [HttpPost(Name = "CheckoutOrder")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<Response<int>>> CheckoutOrder([FromBody] CreateCheckoutOrderCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPut(Name = "UpdateOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        var result = await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteOrder")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var cmd = new DeleteOrderCommand(id);
        await Mediator.Send(cmd);
        return NoContent();
    }
}