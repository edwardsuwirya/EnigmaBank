using Application.Repositories;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class Startup
{
    public static IServiceCollection AddTokenUtils(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton(typeof(IJwtManagerRepository), new JwtManagerRepository(configuration));
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient(typeof(IReadRepositoryAsync<,>), typeof(ReadRepositoryAsync<,>))
            .AddTransient(typeof(IWriteRepositoryAsync<,>), typeof(WriteRepositoryAsync<,>))
            .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
    }
}