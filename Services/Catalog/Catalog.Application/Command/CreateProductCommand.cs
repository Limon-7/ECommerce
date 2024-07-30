using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Command;

public class CreateProductCommand: IRequest<Product>
{
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public decimal Price { get; set; }
    public Brand Brands { get; set; }
    public ProductType Types { get; set; }
}

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = new Product()
        {
            Name = request.Name,
            Summary = request.Summary,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price,
            Brands = request.Brands,
            Types = request.Types
        };
        /*if (productEntity is null)
        {
            throw new ApplicationException("There is an issue with mapping while creating new product");
        }*/

        var newProduct = await _productRepository.CreateProduct(productEntity);
        return newProduct;
    }
}