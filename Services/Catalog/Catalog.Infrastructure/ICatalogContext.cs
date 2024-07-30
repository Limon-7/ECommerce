using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure;

public interface ICatalogContext
{
    IMongoCollection<Product> Products { get; }
    IMongoCollection<Brand> Brands { get; }
    IMongoCollection<ProductType> Types { get; }
}