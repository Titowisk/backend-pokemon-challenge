using PokemonDomain.PokemonModel;

namespace PokemonApplication.PokemonService
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetRandomPokemonsAsync(int quantity);
    }
}
