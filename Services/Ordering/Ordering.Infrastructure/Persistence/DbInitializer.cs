using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Persistence;

public class DbInitializer
{
    public static async Task SeedDatabase(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<OrderDbContext>();
            await context.Database.MigrateAsync(); // Apply migrations asynchronously
            await SeedOrderDbContext.SeedAsync(context); // Seed data asynchronously
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<DbInitializer>>();
            logger.LogError(ex, "An error occurred seeding the database.");
        }
    }
}