using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Domain.Specs;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository: IProductRepository, IProductBrandRepository, IProductTypeRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }
    public Task<IQueryable<Product>> GetProducts(CatalogSpecParams catalogSpecParams)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProduct(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductByBrand(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
        return product;
    }

    public Task<bool> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Brand>> GetAllBrands()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductType>> GetAllTypes()
    {
        throw new NotImplementedException();
    }
}