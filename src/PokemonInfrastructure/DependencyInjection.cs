using Microsoft.Extensions.DependencyInjection;
using PokemonApplication.Persistence;
using PokemonInfrastructure.Persistence;
using PokemonInfrastructure.Persistence.Repository;
using PokemonInfrastructure.Persistence.Seed;

namespace PokemonInfrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSqliteConfiguration();
        services.AddTransient<PokemonDatabaseSeeder>();
        services.AddRepository();
        return services;
    }

    public static IServiceCollection AddSqliteConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<PokemonContext>();
        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IPokemonRepository, PokemonRepository>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        return services;
    }
}
