using System.Collections;
using AutoMapper;
using Catalog.Application.Common.Models;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Products.Queries.GetProductByProductName;

public class GetProductByProductNameQuery: IRequest<Response<IList<ProductResponse>>>
{
    public string Name { get; set; }

    public GetProductByProductNameQuery(string name) => Name = name;
}

public class GetProductByProductNameQueryHandler: IRequestHandler<GetProductByProductNameQuery, Response<IList<ProductResponse>>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByProductNameQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Response<IList<ProductResponse>>> Handle(GetProductByProductNameQuery request, CancellationToken cancellationToken)
    {
        
        return Response.Ok(_mapper.Map<IList<ProductResponse>>(await _repository.GetProductsByProductName(request.Name)));

    }
}