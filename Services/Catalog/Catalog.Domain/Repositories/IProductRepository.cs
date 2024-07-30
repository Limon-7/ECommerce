using Catalog.Domain.Entities;
using Catalog.Domain.Specs;

namespace Catalog.Domain.Repositories;

public interface IProductRepository
{
    Task<IQueryable<Product>> GetProducts(CatalogSpecParams catalogSpecParams);
    Task<Product> GetProduct(string id);
    Task<IEnumerable<Product>> GetProductByName(string name);
    Task<IEnumerable<Product>> GetProductByBrand(string name);
    Task<Product> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}