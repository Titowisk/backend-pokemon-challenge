using Microsoft.AspNetCore.Mvc;
using PokemonApplication.PokemonService;
using PokemonContracts.DTOs;

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

    [HttpGet]
    public async Task<ActionResult<List<PokemonDto>>> GetRandomPokemons([FromQuery] int random)
    {
        List<PokemonDto> randomPokemons = await _pokemonService.GetRandomPokemonsAsync(random);
        return Ok(randomPokemons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PokemonDto>> GetPokemonById(int id)
    {
        var pokemon = await _pokemonService.GetById(id);
        return Ok(pokemon);
    }
}
