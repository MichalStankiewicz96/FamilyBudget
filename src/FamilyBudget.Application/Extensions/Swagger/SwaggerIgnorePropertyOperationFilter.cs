using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FamilyBudget.Application.Extensions.Swagger;
public sealed class SwaggerIgnorePropertyOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription == null || operation.Parameters == null)
        {
            return;
        }
        if (!context.ApiDescription.ParameterDescriptions.Any())
        {
            return;
        }
        var excludedProperties = context.ApiDescription.ParameterDescriptions.Where(p => p.Source.Equals(BindingSource.Form) ||
                p.Source.Equals(BindingSource.Query) ||
                p.Source.Equals(BindingSource.Body))
            .ToList();
        if (excludedProperties.Any())
        {
            foreach (var excludedProperty in excludedProperties)
            {
                foreach (var customAttribute in excludedProperty.CustomAttributes())
                {
                    if (customAttribute.GetType() == typeof(SwaggerIgnoreAttribute))
                    {
                        if (operation.RequestBody is not null)
                        {
                            IgnoreRequestBodyValues(operation.RequestBody.Content.Values, excludedProperty.Name);
                        }
                        else
                        {
                            operation.Parameters = IgnoreParameterValues(operation.Parameters, excludedProperty.Name);
                        }
                    }
                }
            }
        }
    }

    private static void IgnoreRequestBodyValues(ICollection<OpenApiMediaType> values, string valueName)
    {
        for (var i = 0; i < values.Count; i++)
        {
            for (var j = 0; j < values.ElementAt(i).Encoding.Count; j++)
            {
                if (values.ElementAt(i).Encoding.ElementAt(j).Key == valueName)
                {
                    values.ElementAt(i)
                        .Encoding
                        .Remove(values.ElementAt(i)
                            .Encoding
                            .ElementAt(j));
                    values.ElementAt(i)
                        .Schema
                        .Properties
                        .Remove(valueName);
                }
            }
        }
    }

    private static IList<OpenApiParameter> IgnoreParameterValues(IEnumerable<OpenApiParameter> parameters, string valueName)
    {
        return parameters.Where(p => !p.Name.Equals(valueName)).ToList();
    }
}

