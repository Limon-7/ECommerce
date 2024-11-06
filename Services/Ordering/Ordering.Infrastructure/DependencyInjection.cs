using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("defaultConnection")));
        services.AddHealthChecks()
            .AddSqlServer(configuration["ConnectionStrings:defaultConnection"]!, "SELECT 1", "Sql Server",
                HealthStatus.Degraded);
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}