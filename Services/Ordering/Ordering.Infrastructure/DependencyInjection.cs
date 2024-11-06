using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Services;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("defaultConnection"), b 
                => b.MigrationsAssembly(typeof(OrderDbContext).Assembly.FullName)));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<OrderDbContext>());
        
        services.AddHealthChecks()
            .AddSqlServer(configuration["ConnectionStrings:defaultConnection"]!, "SELECT 1", "Sql Server",
                HealthStatus.Degraded);
        
        services.AddScoped<IOrderService, OrderService>();
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}