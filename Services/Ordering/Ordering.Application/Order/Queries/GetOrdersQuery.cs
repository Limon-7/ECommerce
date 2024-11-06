using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Application.Order.Responses;

namespace Ordering.Application.Order.Queries;

public class GetOrdersQuery : IRequest<Response<List<OrderResponse>>>
{
    public GetOrdersQuery(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; }
}

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Response<List<OrderResponse>>>
{
    private readonly ILogger<GetOrdersQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IOrderService _service;

    public GetOrdersQueryHandler(ILogger<GetOrdersQueryHandler> logger, IOrderService service, IMapper mapper)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
    }

    public async Task<Response<List<OrderResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _service.GetOrdersByUserNameAsync(request.UserName);
        var data = _mapper.Map<List<OrderResponse>>(orders);
        return Response.Ok(data);
    }
}