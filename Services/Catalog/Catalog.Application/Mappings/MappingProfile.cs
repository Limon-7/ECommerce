using AutoMapper;
using Catalog.Application.Brands.GetBrands;
using Catalog.Application.Products.Queries;
using Catalog.Application.ProductTypes.GetProductTypes;
using Catalog.Domain.Entities;
using Catalog.Domain.Specs;

namespace Catalog.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductResponse>().ReverseMap();
        // CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Brand, BrandResponse>().ReverseMap();
        CreateMap<ProductType, ProductTypeResponse>().ReverseMap();
        CreateMap<PaginatedList<Product>, PaginatedList<ProductResponse>>().ReverseMap();
    }
}