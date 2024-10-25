using Microsoft.Extensions.DependencyInjection;
using PokemonApplication.PokemonService;
using PokemonApplication.TrainerService;

namespace PokemonApplication;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPokemonService, PokemonService.PokemonService>();
        services.AddScoped<ITrainerService, TrainerService.TrainerService>();
        return services;
    }
}
