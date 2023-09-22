using FamilyBudget.Application;
using FamilyBudget.Application.Extensions.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace FamilyBudget.Api.Extensions.Swagger;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Specify the JWT token.",
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Type = SecuritySchemeType.Http
            });
            opt.ExampleFilters();
            opt.SchemaFilter<SwaggerIgnorePropertySchemaFilter>();
            opt.OperationFilter<SwaggerIgnorePropertyOperationFilter>();
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    new string[] { }
                }
            });
            opt.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            opt.SupportNonNullableReferenceTypes();
        });
        services.AddSwaggerExamplesFromAssemblyOf<IApiMarker>();
        return services;
    }

    public static void UseSwaggerUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
