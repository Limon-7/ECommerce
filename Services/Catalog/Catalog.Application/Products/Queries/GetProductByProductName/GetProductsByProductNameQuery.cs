using AutoMapper;
using Catalog.Application.Common.Models;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Products.Queries.GetProductByProductName;

public class GetProductsByProductNameQuery : IRequest<Response<IList<ProductResponse>>>
{
    public string Name { get; set; }

    public GetProductsByProductNameQuery(string name) => Name = name;
}

public class
    GetProductsByProductNameQueryHandler : IRequestHandler<GetProductsByProductNameQuery,
        Response<IList<ProductResponse>>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductsByProductNameQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<IList<ProductResponse>>> Handle(GetProductsByProductNameQuery request,
        CancellationToken cancellationToken)
    {
        return Response.Ok(
            _mapper.Map<IList<ProductResponse>>(await _repository.GetProductsByProductName(request.Name)));
    }
}