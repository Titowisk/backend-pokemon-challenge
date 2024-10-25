using PokemonApplication.Persistence;
using PokemonDomain.PokemonModel;

namespace PokemonApplication.PokemonService;
public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<List<Pokemon>> GetRandomPokemonsAsync(int quantity)
    {
        var allPokemons = await _pokemonRepository.GetAll();

        var random = new Random();
        var randomPokemons = allPokemons.OrderBy(x => random.Next())
            .Take(quantity)
            .ToList();

        return randomPokemons;
    }

    public async Task<Pokemon> GetById(int id)
    {
        var pokemon = await _pokemonRepository.GetById(id);

        return pokemon == null ? 
            throw new NullReferenceException($"Pokemon with id {id} not found") : pokemon;
    }
}
