using Microsoft.Extensions.DependencyInjection;
using PokemonInfrastructure.Persistence;
using PokemonInfrastructure.Persistence.Seed;

namespace PokemonInfrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSqliteConfiguration();
        services.AddTransient<PokemonDatabaseSeeder>();
        return services;
    }

    public static IServiceCollection AddSqliteConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<PokemonContext>();
        return services;
    }
}
