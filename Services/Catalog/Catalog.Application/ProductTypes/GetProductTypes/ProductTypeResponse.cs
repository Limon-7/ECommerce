namespace Catalog.Application.ProductTypes.GetProductTypes;

public class ProductTypeResponse
{
    public string Id { get; set; }
    public string Name { get; set; }

    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}