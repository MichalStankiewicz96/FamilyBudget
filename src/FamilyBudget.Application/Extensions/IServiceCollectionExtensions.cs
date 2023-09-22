using FamilyBudget.Persistence;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace FamilyBudget.Application.Extensions;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(IApplicationMarker));
        services.AddMapper();
        services.AddFluentValidation();
        services.AddDbContext(configuration);
        return services;
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(configuration.GetConnectionString("FamilyBudgetDb")),
                optionsLifetime: ServiceLifetime.Singleton)
            .AddDbContextFactory<ApplicationDbContext>();
    }

    private static void AddMapper(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();
        config.Scan(Assembly.GetAssembly(typeof(IApplicationMarker))!);
        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();
    }
}