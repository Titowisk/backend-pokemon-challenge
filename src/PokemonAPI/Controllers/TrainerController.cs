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

    // Get: Get all trainers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trainer>>> Get()
    {
        return Ok();
    }

    // GetById: Get a trainer by id
    [HttpGet("{id}")]
    public async Task<ActionResult<Trainer>> GetById(int id)
    {
        var trainer = await _trainerService.GetById(id);

        return Ok(trainer);
    }

    // GetCapturedPokemons: Get all captured pokemons of a trainer
    [HttpGet("{id}/captured-pokemons")]
    public async Task<ActionResult<IEnumerable<Pokemon>>> GetCapturedPokemons(int id)
    {
        var pokemons = await _trainerService.GetCapturedPokemons(id);
        return Ok(pokemons);
    }

    // CapturePokemon: Capture a pokemon for a trainer
    [HttpPost("{id}/pokemon/{pokemonId}")]
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
