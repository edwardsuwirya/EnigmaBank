using System.Reflection;
using Application.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Startup
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services.AddValidatorsFromAssembly(assembly);
    }
}