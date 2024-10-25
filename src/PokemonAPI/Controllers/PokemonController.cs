using Microsoft.AspNetCore.Mvc;
using PokemonApplication.PokemonService;

namespace PokemonAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    // Get: 10 random pokemons
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetRandomPokemons([FromQuery] int random)
    {
        var randomPokemons = await _pokemonService.GetRandomPokemonsAsync(random);
        return Ok(randomPokemons);
    }

    // GetById: Get a pokemon by id
    [HttpGet("{id}")]
    public async Task<ActionResult<string>> GetPokemonById(int id)
    {
        var pokemon = await _pokemonService.GetById(id);
        return Ok(pokemon);
    }

    // GetCapturedPokemons: Get all captured pokemons

    // CapturePokemon: Capture a pokemon
}
