using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Runtime.Versioning;

namespace Company.Delivery.Api.AppStart;

internal static class DeliveryApiConfig
{
    public static void AddDeliveryApi(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            var targetFramework = executingAssembly.GetCustomAttribute<TargetFrameworkAttribute>();
            var frameworkName = targetFramework is null ? string.Empty : " (" + targetFramework.FrameworkDisplayName + ")";
            var description = "An ASP.NET Core" + frameworkName + " Web API";

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Delivery API",
                Description = description
            });

            var xmlFilename = $"{executingAssembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));
    }

    public static void UseDeliveryApi(this IApplicationBuilder builder)
    {
        builder.UseSwagger();
        builder.UseSwaggerUI(options =>
        {
            options.DocumentTitle = "Delivery API";

            options.DefaultModelsExpandDepth(0);
            options.DisplayRequestDuration();
            options.DocExpansion(DocExpansion.None);
        });
    }
}