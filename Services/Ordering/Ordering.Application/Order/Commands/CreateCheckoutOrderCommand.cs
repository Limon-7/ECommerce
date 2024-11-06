using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;

namespace Ordering.Application.Order.Commands;

public class CreateCheckoutOrderCommand : IRequest<Response<int>>
{
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

public class CreateCheckoutOrderCommandHandler : IRequestHandler<CreateCheckoutOrderCommand, Response<int>>
{
    private readonly ILogger<CreateCheckoutOrderCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderService _service;

    public CreateCheckoutOrderCommandHandler(ILogger<CreateCheckoutOrderCommandHandler> logger, IOrderService service,
        IMapper mapper)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
    }

    public async Task<Response<int>> Handle(CreateCheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<Domain.Entities.Order>(request);
            await _service.AddAsync(entity);
            return Response.Ok(entity.Id);
        }
        catch (Exception e)
        {
            return Response.Fail<int>(e.Message);
        }
    }
}