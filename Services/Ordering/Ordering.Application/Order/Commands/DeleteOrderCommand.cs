using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Exceptions;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;

namespace Ordering.Application.Order.Commands;

public class DeleteOrderCommand : IRequest<Response<int>>
{
    public DeleteOrderCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<int>>
{
    private readonly ILogger<DeleteOrderCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderService _service;

    public DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger, IOrderService service, IMapper mapper)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
    }

    public async Task<Response<int>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _service.GetByIdAsync(request.Id);
        //
        if (order == null) throw new NotFoundException(nameof(order), request.Id);
        await _service.DeleteAsync(order);
        _logger.LogInformation($"Order with Id {request.Id} is deleted successfully.");
        return Response.Ok(1);
    }
}