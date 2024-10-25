using Microsoft.Extensions.DependencyInjection;
using PokemonApplication.PokemonService;

namespace PokemonApplication;
public static class DependencyInjection
{
    // encapsulate the logic for registering services for this layer
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPokemonService, PokemonService.PokemonService>();
        return services;
    }
}
