using Microsoft.AspNetCore.Mvc;
using PokemonApplication.TrainerService;
using PokemonContracts.DTOs;
using PokemonDomain.PokemonModel;
using PokemonDomain.TrainerModel;

namespace PokemonAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainerController : ControllerBase
{
    private readonly ITrainerService _trainerService;

    public TrainerController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TrainerDto>> GetById(int id)
    {
        var trainer = await _trainerService.GetById(id);

        return Ok(trainer);
    }

    [HttpGet("{id}/captured-pokemons")]
    public async Task<ActionResult<IEnumerable<CapturedPokemonDto>>> GetCapturedPokemons(int id)
    {
        var pokemons = await _trainerService.GetCapturedPokemons(id);
        return Ok(pokemons);
    }

    [HttpPost("{id}/capture-pokemon/{pokemonId}")]
    public async Task<ActionResult> CapturePokemon(int id, int pokemonId)
    {
        await _trainerService.CapturePokemon(id, pokemonId);
        return NoContent();
    }

    /// <summary>
    /// Create a new trainer
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Trainer>> Create([FromBody] CreateTrainerDto dto)
    {
        await _trainerService.CreateTrainerAsync(dto.Name, dto.Age, dto.CPF);

        return NoContent();
    }
}
