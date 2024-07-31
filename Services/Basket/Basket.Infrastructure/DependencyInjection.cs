using Basket.Application.Common.Interfaces;
using Basket.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Basket.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
        });
        services.AddHealthChecks()
            .AddRedis(configuration["CacheSettings:ConnectionString"]!, "Redis Health", HealthStatus.Degraded);
        services.AddScoped<IBasketService, BasketService>();
        return services;
    }
}