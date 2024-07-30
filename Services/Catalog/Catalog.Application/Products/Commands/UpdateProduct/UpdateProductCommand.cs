using AutoMapper;
using Catalog.Application.Common.Models;
using Catalog.Application.Products.Queries;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<Response<ProductResponse>>
{
    public string Id { get; }
    public string Name { get; }
    public string Summary { get; }
    public string Description { get; }
    public string ImageFile { get; }
    public decimal Price { get; }
    public Brand Brands { get; }
    public ProductType Types { get; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<ProductResponse>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<ProductResponse>> Handle(UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetProductById(request.Id);
        if (entity == null)
            return Response.Fail<ProductResponse>($"Not found");

        entity.Description = request.Description;
        entity.Brands = request.Brands;
        entity.ImageFile = request.ImageFile;
        entity.Name = request.Name;
        entity.Types = request.Types;
        entity.Summary = request.Summary;
        entity.Price = request.Price;
        entity.LastModified = DateTime.UtcNow;
        entity.LastModifiedBy = "SystemUser";

        try
        {
            await _repository.UpdateProduct(entity);
            return Response.Ok(_mapper.Map<ProductResponse>(entity));
        }
        catch (Exception e)
        {
            return Response.Fail<ProductResponse>($"System Error: {e.Message}");
        }
    }
}