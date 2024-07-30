using AutoMapper;
using Catalog.Application.Common.Models;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Products.Queries.GetProductsByBrandName;

public class GetProductsByBrandNameQuery: IRequest<Response<IList<ProductResponse>>>
{
    public string BrandName { get; set; }

    public GetProductsByBrandNameQuery(string brandName)
    {
        BrandName = brandName;
    }
}

public class GetProductsByBrandNameQueryHandler : IRequestHandler<GetProductsByBrandNameQuery, Response<IList<ProductResponse>>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductsByBrandNameQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Response<IList<ProductResponse>>> Handle(GetProductsByBrandNameQuery request, CancellationToken cancellationToken)
    {
        return Response.Ok(_mapper.Map<IList<ProductResponse>>(await _repository.GetProductByBrandName(request.BrandName)));
    }
}