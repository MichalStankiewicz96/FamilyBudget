using FamilyBudget.Application.Configuration.Options;

namespace FamilyBudget.Api.Extensions.Options;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions
{
    public static ConfigurationManager AddApplicationOptions(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        return configuration;
    }
}
