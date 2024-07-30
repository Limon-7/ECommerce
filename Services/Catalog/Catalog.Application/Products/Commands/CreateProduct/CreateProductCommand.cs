using Catalog.Application.Common.Models;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Products.Commands.CreateProduct;

public class CreateProductCommand: IRequest<Response<bool>>
{
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public decimal Price { get; set; }
    public Brand Brands { get; set; }
    public ProductType Types { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<bool>>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<Response<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = new Product()
        {
            Name = request.Name,
            Summary = request.Summary,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price,
            Brands = request.Brands,
            Types = request.Types,
            Created = DateTime.UtcNow
        };
        try
        {
            await _repository.CreateProduct(newProduct);
            return Response.Ok<bool>(true,newProduct.Id);
        }
        catch (Exception e)
        {
            return Response.Fail<bool>(e.Message);
        }
    }
}