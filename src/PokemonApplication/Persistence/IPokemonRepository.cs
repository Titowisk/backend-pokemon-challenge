using PokemonDomain.PokemonModel;

namespace PokemonApplication.Persistence;
public interface IPokemonRepository
{
    Task<List<Pokemon>> GetAll();
}
