using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Coffee.Core.Application.Common.Interfaces;
using Coffee.Infrastructure.Persistence;

namespace Coffee.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<CoffeeContext>(options =>
                options.UseInMemoryDatabase("DefaultConnection").UseLazyLoadingProxies());
        }
        else
        {
            services.AddDbContext<CoffeeContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(CoffeeContext).Assembly.FullName))
                    .UseLazyLoadingProxies());
        }

        services.AddScoped<ICoffeeContext, CoffeeContext>();

        services.AddScoped<CoffeeDbContextInitializer>();

        return services;
    }
}