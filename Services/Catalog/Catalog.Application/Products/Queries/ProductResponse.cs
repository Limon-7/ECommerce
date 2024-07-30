using Catalog.Domain.Entities;

namespace Catalog.Application.Products.Queries;

public class ProductResponse
{
    public string Id { get; set; }
    public string? LastModifiedBy { get; set; }
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public Brand Brands { get; set; }
    public ProductType Types { get; set; }
    public decimal Price { get; set; }
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
}