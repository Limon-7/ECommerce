using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface IProductTypeRepository
{
    Task<IEnumerable<ProductType>> GetProductTypes();

}