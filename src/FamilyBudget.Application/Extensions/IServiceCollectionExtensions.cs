using FamilyBudget.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudget.Application.Extensions;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        //register other services
        services.AddDbContext(configuration);
        return services;
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(configuration.GetConnectionString("FamilyBudgetDb")),
                optionsLifetime: ServiceLifetime.Singleton)
            .AddDbContextFactory<ApplicationDbContext>();
    }
}