using AutoMapper;
using Catalog.Application.Common.Models;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.ProductTypes.GetProductTypes;

public class GetProductTypesQuery : IRequest<Response<IList<ProductTypeResponse>>>
{
}

public class GetProductTypesQueryHandler : IRequestHandler<GetProductTypesQuery, Response<IList<ProductTypeResponse>>>
{
    private readonly IProductTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetProductTypesQueryHandler(IProductTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<IList<ProductTypeResponse>>> Handle(GetProductTypesQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _repository.GetProductTypes();
        return Response.Ok<IList<ProductTypeResponse>>(_mapper.Map<IList<ProductTypeResponse>>(response));
    }
}