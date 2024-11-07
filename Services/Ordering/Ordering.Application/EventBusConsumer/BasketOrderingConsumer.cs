using AutoMapper;
using EventBus.Service.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Order.Commands;

namespace Ordering.Application.EventBusConsumer;

public class BasketOrderingConsumer: IConsumer<BasketCheckoutPlacedEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketOrderingConsumer> _logger;

    public BasketOrderingConsumer(IMediator mediator, IMapper mapper, ILogger<BasketOrderingConsumer> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<BasketCheckoutPlacedEvent> context)
    {
        var command = _mapper.Map<CreateCheckoutOrderCommand>(context.Message);
        var result = await _mediator.Send(command);
        _logger.LogInformation($"Basket checkout event completed!!!");
    }
}