using Catalog.Domain.Entities;
using Catalog.Domain.Specs;

namespace Catalog.Domain.Repositories;

public interface IProductRepository
{
    Task<PaginatedList<Product>> GetProducts(CatalogSpecParams catalogSpecParams);
    Task<Product> GetProductById(string id);
    Task<IEnumerable<Product>> GetProductsByProductName(string name);
    Task<IEnumerable<Product>> GetProductByBrandName(string name);
    Task<Product> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(string id);
}