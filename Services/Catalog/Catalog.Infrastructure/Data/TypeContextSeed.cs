using System.Text.Json;
using Catalog.Domain.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class TypeContextSeed
{
    private readonly ILogger<TypeContextSeed> _logger;

    public TypeContextSeed(ILogger<TypeContextSeed> logger)
    {
        _logger = logger;
    }
    public static void SeedTypeData(IMongoCollection<ProductType> typeCollection)
    {
        bool checkTypes = typeCollection.Find(b => true).Any();
        if (!checkTypes)
        {
            // string path = Path.Combine(Environment.CurrentDirectory, "Data", "SeedData", "types.json");
            // path = "../Catalog.Infrastructure/Data/SeedData/types.json"; //use for local development
            // var typesData = File.ReadAllText(path);
            
            string typesData = @"[
            {""Name"":""Shoes"",""Id"":""63ca5d4bc3a8a58f47299f97"",""Created"":""2024-07-31T06:59:14.0520173Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:59:14.0520179Z"",""LastModifiedBy"":null},
            {""Name"":""Rackets"",""Id"":""63ca5d6d958e43ee1cd375fe"",""Created"":""2024-07-31T06:59:14.0524476Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:59:14.0524478Z"",""LastModifiedBy"":null},
            {""Name"":""Football"",""Id"":""63ca5d7d380402dce7f06ebc"",""Created"":""2024-07-31T06:59:14.0524578Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:59:14.0524578Z"",""LastModifiedBy"":null},
            {""Name"":""Kit Bags"",""Id"":""63ca5d8849bc19321b8be5f1"",""Created"":""2024-07-31T06:59:14.0524614Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:59:14.0524615Z"",""LastModifiedBy"":null}
        ]";
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            
            if (types != null)
            {
                foreach (var item in types)
                {
                    typeCollection.InsertOneAsync(item);
                }
            }
        }
    }
}