using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokemonInfrastructure.Persistence;

namespace PokemonInfrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSqliteConfiguration();
        return services;
    }

    public static IServiceCollection AddSqliteConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<PokemonContext>();
        return services;
    }
}
