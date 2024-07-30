using AutoMapper;
using Catalog.Application.Common.Models;
using Catalog.Domain.Repositories;
using Catalog.Domain.Specs;
using MediatR;

namespace Catalog.Application.Products.Queries.GetProductsWithPagination;

public class GetProductsWithPaginationQuery : IRequest<Response<PaginatedList<ProductResponse>>>
{
    public CatalogSpecParams CatalogSpecParams { get; set; }

    public GetProductsWithPaginationQuery(CatalogSpecParams @params)
    {
        CatalogSpecParams = @params;
    }
}

public class
    GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery,
        Response<PaginatedList<ProductResponse>>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductsWithPaginationQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<PaginatedList<ProductResponse>>> Handle(GetProductsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var paginatedProducts = await _repository.GetProducts(request.CatalogSpecParams);
        return Response.Ok<PaginatedList<ProductResponse>>(
            _mapper.Map<PaginatedList<ProductResponse>>(paginatedProducts));
    }
}