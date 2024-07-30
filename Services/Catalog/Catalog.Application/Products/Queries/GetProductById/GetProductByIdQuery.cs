using AutoMapper;
using Catalog.Application.Common.Models;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<Response<ProductResponse>>
{
    public string Id { get; }

    public GetProductByIdQuery(string id)
    {
        Id = id;
    }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<ProductResponse>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<ProductResponse>> Handle(GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        return Response.Ok<ProductResponse>(_mapper.Map<ProductResponse>(await _repository.GetProductById(request.Id)));
    }
}