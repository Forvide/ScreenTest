using Company.Delivery.Domain;
using Company.Delivery.Infrastructure;

namespace Company.Delivery.Api.AppStart;

internal static class DependencyInjectionConfig
{
    public static void AddDependencyInjection(this IServiceCollection services)
    { 
        services.AddScoped<IWaybillService, WaybillService>();
    }
}