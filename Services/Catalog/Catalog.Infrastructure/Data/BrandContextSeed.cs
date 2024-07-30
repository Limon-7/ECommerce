using System.Text.Json;
using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class BrandContextSeed
{
    public static void SeedBrandData(IMongoCollection<Brand> brandCollection)
    {
        bool checkBrands = brandCollection.Find(b => true).Any();
        string path = Path.Combine("Data", "SeedData", "brands.json");
        if (!checkBrands)
        {
            path = "../Catalog.Infrastructure/Data/SeedData/brands.json";//use for local development
            var brandsData = File.ReadAllText(path);
            var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);
            if (brands != null)
            {
                foreach (var item in brands)
                {
                    brandCollection.InsertOneAsync(item);
                }
            }
        }
    } 
}