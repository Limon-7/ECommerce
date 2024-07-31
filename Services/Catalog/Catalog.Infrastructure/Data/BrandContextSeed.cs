using System.Text.Json;
using Catalog.Domain.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class BrandContextSeed
{
    public BrandContextSeed()
    {
        
    }
    public static void SeedBrandData(IMongoCollection<Brand> brandCollection)
    {
        bool checkBrands = brandCollection.Find(b => true).Any();
        if (!checkBrands)
        {
            // string path = Path.Combine( Environment.CurrentDirectory, "Data", "SeedData", "brands.json");
            // path = "../Catalog.Infrastructure/Data/SeedData/brands.json";//use for local development
            // var brandsData = File.ReadAllText(path);
            // var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);
            // Console.Write(JsonSerializer.Serialize(brands));
            string brandsData= @"[
            {""Name"":""Adidas"",""Id"":""63ca5e40e0aa3968b549af53"",""Created"":""2024-07-31T06:51:51.9630539Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:51:51.9630549Z"",""LastModifiedBy"":null},
            {""Name"":""ASICS"",""Id"":""63ca5e4c455900b990b43bc1"",""Created"":""2024-07-31T06:51:51.9679308Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:51:51.9679313Z"",""LastModifiedBy"":null},
            {""Name"":""Victor"",""Id"":""63ca5e59065163c16451bd73"",""Created"":""2024-07-31T06:51:51.9679504Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:51:51.9679507Z"",""LastModifiedBy"":null},
            {""Name"":""Yonex"",""Id"":""63ca5e655ec1fdc49bd9327d"",""Created"":""2024-07-31T06:51:51.9679571Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:51:51.9679572Z"",""LastModifiedBy"":null},
            {""Name"":""Puma"",""Id"":""63ca5e728c4cff9708ada2a6"",""Created"":""2024-07-31T06:51:51.9679625Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:51:51.9679626Z"",""LastModifiedBy"":null},
            {""Name"":""Nike"",""Id"":""63ca5e7ec90ff5c8f44d5ac8"",""Created"":""2024-07-31T06:51:51.967969Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:51:51.9679692Z"",""LastModifiedBy"":null},
            {""Name"":""Babolat"",""Id"":""63ca5e8d6110a9c56ee7dc48"",""Created"":""2024-07-31T06:51:51.9679735Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T06:51:51.9679738Z"",""LastModifiedBy"":null}
        ]";
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