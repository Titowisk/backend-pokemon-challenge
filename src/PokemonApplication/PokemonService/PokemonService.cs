using PokemonApplication.Persistence;
using PokemonContracts.DTOs;
using PokemonDomain.PokemonModel;

namespace PokemonApplication.PokemonService;
public class PokemonService : IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonService(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<List<PokemonDto>> GetRandomPokemonsAsync(int quantity)
    {
        var allPokemons = await _pokemonRepository.GetAll();

        var random = new Random();
        var randomPokemons = allPokemons.OrderBy(x => random.Next())
            .Take(quantity)
            .ToList();

        var randomPokemonsDto = randomPokemons.Select(x => new PokemonDto
        {
            Id = x.Id,
            Name = x.Name,
            Types = x.Types.Select(t => t.Name).ToList(),
            Evolutions = x.Evolutions.Select(e => e.Name).ToList(),
            Involutions = x.Involutions.Select(i => i.Name).ToList()
        }).ToList();

        return randomPokemonsDto;
    }

    public async Task<PokemonDto> GetById(int id)
    {
        var pokemon = await _pokemonRepository.GetById(id);

        if (pokemon == null)
            throw new NullReferenceException($"Pokemon with id {id} not found");

        return new PokemonDto
        {
            Id = id,
            Name = pokemon.Name,
            Types = pokemon.Types.Select(t => t.Name).ToList(),
            Evolutions = pokemon.Evolutions.Select(e => e.Name).ToList(),
            Involutions = pokemon.Involutions.Select(i => i.Name).ToList()
        };
    }
}
