using PokemonDomain.PokemonModel;

namespace PokemonApplication.PokemonService
{
    public interface IPokemonService
    {
        Task<Pokemon> GetById(int id);
        Task<List<Pokemon>> GetRandomPokemonsAsync(int quantity);
    }
}
