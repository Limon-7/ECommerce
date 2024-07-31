using System.Text.Json;
using Catalog.Domain.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContextSeed
{
    public static void SeedCatalogData(IMongoCollection<Product> productCollection)
    {
        bool checkProducts = productCollection.Find(b => true).Any();

        if (!checkProducts)
        {
            // string path = Path.Combine(Environment.CurrentDirectory, "Data", "SeedData", "products.json");
            // path = "../Catalog.Infrastructure/Data/SeedData/products.json"; //use for local development
            //
            // var productsData = File.ReadAllText(path);
            var productsData =
                @"[{""Name"":""Adidas Quick Force Indoor Badminton Shoes"",""Summary"":""Adidas Quick Force Indoor Badminton Shoes"",""Description"":""Designed for professional as well as amateur badminton players. These indoor shoes are crafted with synthetic upper that provides natural fit, while the EVA midsole provides lightweight cushioning. The shoes can be used for Badminton and Squash"",""ImageFile"":""images/products/adidas_shoe-1.png"",""Brands"":{""Name"":""Adidas"",""Id"":""63ca5e40e0aa3968b549af53"",""Created"":""2024-07-31T07:08:33.8135097Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8135109Z"",""LastModifiedBy"":null},""Types"":{""Name"":""Shoes"",""Id"":""63ca5d4bc3a8a58f47299f97"",""Created"":""2024-07-31T07:08:33.8109266Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8109276Z"",""LastModifiedBy"":null},""Price"":3500,""Id"":""602d2149e773f2a3990b47f5"",""Created"":""2024-07-31T07:08:33.7981607Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.7981622Z"",""LastModifiedBy"":null},
        {""Name"":""Asics Gel Rocket 8 Indoor Court Shoes"",""Summary"":""Asics Gel Rocket 8 Indoor Court Shoes"",""Description"":""The Asics Gel Rocket 8 Indoor Court Shoes (Orange/Silver) will keep you motivated and fired up to perform at your peak in volleyball, squash and other indoor sports. Beginner and intermediate players get cutting-edge technologies at an affordable price point.This entry level all-rounder has a durable upper and offers plenty of stability."",""ImageFile"":""images/products/asics_shoe-1.png"",""Brands"":{""Name"":""ASICS"",""Id"":""63ca5e4c455900b990b43bc1"",""Created"":""2024-07-31T07:08:33.8146815Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8146817Z"",""LastModifiedBy"":null},""Types"":{""Name"":""Shoes"",""Id"":""63ca5d4bc3a8a58f47299f97"",""Created"":""2024-07-31T07:08:33.8146715Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8146717Z"",""LastModifiedBy"":null},""Price"":4249,""Id"":""602d2149e773f2a3990b47f8"",""Created"":""2024-07-31T07:08:33.8145422Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8145427Z"",""LastModifiedBy"":null},
        {""Name"":""Victor SHW503 F Badminton Shoes"",""Summary"":""Victor SHW503 F Badminton Shoes"",""Description"":""PU Leather, Mesh,EVA, ENERGY MAX, Nylon sheet, Rubber"",""ImageFile"":""images/products/victor_shoe-1.png"",""Brands"":{""Name"":""Victor"",""Id"":""63ca5e59065163c16451bd73"",""Created"":""2024-07-31T07:08:33.8147162Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8147164Z"",""LastModifiedBy"":null},""Types"":{""Name"":""Shoes"",""Id"":""63ca5d4bc3a8a58f47299f97"",""Created"":""2024-07-31T07:08:33.8147099Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8147101Z"",""LastModifiedBy"":null},""Price"":2392,""Id"":""702d2149e773f2a3990b47fa"",""Created"":""2024-07-31T07:08:33.8146904Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8146907Z"",""LastModifiedBy"":null},
        {""Name"":""Yonex Super Ace Light Badminton Shoes"",""Summary"":""Yonex Super Ace Light Badminton Shoes"",""Description"":""Rule the game with Super Ace Light highlights Maximum shock absorption Quick compression recovery Its high resilience ensure YONEX insoles retain their shape longer Meticulously contoured for comfort Provides stability in the forefoot and toe areas technology."",""ImageFile"":""images/products/yonex_shoe-3.png"",""Brands"":{""Name"":""Yonex"",""Id"":""63ca5e655ec1fdc49bd9327d"",""Created"":""2024-07-31T07:08:33.8147464Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8147466Z"",""LastModifiedBy"":null},""Types"":{""Name"":""Shoes"",""Id"":""63ca5d4bc3a8a58f47299f97"",""Created"":""2024-07-31T07:08:33.8147404Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8147406Z"",""LastModifiedBy"":null},""Price"":3700,""Id"":""922d2149e773f2a3990b47fa"",""Created"":""2024-07-31T07:08:33.8147222Z"",""CreatedBy"":null,""LastModified"":""2024-07-31T07:08:33.8147224Z"",""LastModifiedBy"":null}
     ]";
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products != null)
            {
                foreach (var item in products)
                {
                    productCollection.InsertOneAsync(item);
                }
            }
        }
    }
}