using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Exceptions;
using Ordering.Application.Common.Interfaces;

namespace Ordering.Application.Order.Commands;

public class UpdateOrderCommand : IRequest
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public decimal? TotalPrice { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? CVV { get; set; }
    public int? PaymentMethod { get; set; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly ILogger<UpdateOrderCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderService _service;

    public UpdateOrderCommandHandler(ILogger<UpdateOrderCommandHandler> logger, IOrderService service, IMapper mapper)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _service.GetByIdAsync(request.Id);
        if (orderToUpdate == null) throw new NotFoundException(nameof(Order), request.Id);

        _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Domain.Entities.Order));
        await _service.UpdateAsync(orderToUpdate);
        _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated");
        return Unit.Value;
    }
}