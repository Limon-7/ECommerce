using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface IProductBrandRepository
{
    Task<IEnumerable<Brand>> GetBrands();

}