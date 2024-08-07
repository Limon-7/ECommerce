using Dapper;
using Discount.Domain.Entities;
using Discount.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repository;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Coupon> GetDiscountByProductId(string productId)
    {
        await using var connection =
            new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductId = @ProductId", new { ProductId = productId });
        if (coupon == null)
            return new Coupon(productId: productId, productName: "No Discount", amount: 0,
                description: "No Discount Available");
        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection =
            new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected =
            await connection.ExecuteAsync
            ("INSERT INTO Coupon (ProductId, ProductName, Description, Amount) VALUES (@ProductId, @ProductName, @Description, @Amount)",
                new
                {
                    ProductId = coupon.ProductId, ProductName = coupon.ProductName, Description = coupon.Description,
                    Amount = coupon.Amount
                });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection =
            new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected = await connection.ExecuteAsync
        ("UPDATE Coupon SET ProductId=@ProductId, ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
            new
            {
                ProductId = coupon.ProductId,
                ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount,
                Id = coupon.Id
            });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> DeleteDiscountByProductId(string productId)
    {
        await using var connection =
            new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductId = @ProductId",
            new { ProductId = productId });

        if (affected == 0)
            return false;

        return true;
    }
}