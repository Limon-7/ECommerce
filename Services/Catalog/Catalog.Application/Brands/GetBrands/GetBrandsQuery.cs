using AutoMapper;
using Catalog.Application.Common.Models;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Brands.GetBrands;

public class GetBrandsQuery : IRequest<Response<IList<BrandResponse>>>
{
}

public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, Response<IList<BrandResponse>>>
{
    private readonly IProductBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public GetBrandsQueryHandler(IProductBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<Response<IList<BrandResponse>>> Handle(GetBrandsQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _brandRepository.GetBrands();
        var data = _mapper.Map<IList<BrandResponse>>(response);
        return Response.Ok(data);
    }
}