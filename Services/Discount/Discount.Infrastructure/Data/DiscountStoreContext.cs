using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Data;

public static class DiscountStoreContext
{
    public static void MigrateDatabase<TContext>(this IApplicationBuilder host)
    {
        using var scope = host.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var config = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();
        try
        {
            logger.LogInformation("Discount DB Migration Started");
            ApplyMigrations(config);
            logger.LogInformation("Discount DB Migration Completed");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    
    private static void ApplyMigrations(IConfiguration config)
    {
        using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
        connection.Open();
        using var cmd = new NpgsqlCommand()
        {
            Connection = connection
        };
        cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, ProductId VARCHAR(60) NOT NULL, ProductName VARCHAR(500) NOT NULL, Description TEXT,Amount INT)";
        cmd.ExecuteNonQuery();
        
        cmd.CommandText = "INSERT INTO Coupon(ProductId, ProductName, Description, Amount) VALUES('602d2149e773f2a3990b47f5','Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500)";
        cmd.ExecuteNonQuery();
    }
}