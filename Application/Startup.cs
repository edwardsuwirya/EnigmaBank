using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Startup
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
    }
}