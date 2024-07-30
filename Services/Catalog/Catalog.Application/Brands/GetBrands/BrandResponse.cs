using Catalog.Domain.Common;

namespace Catalog.Application.Brands.GetBrands;

public class BrandResponse : AuditableEntity
{
    public string Id { get; set; }
    public string Name { get; set; }

    public DateTime Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
}