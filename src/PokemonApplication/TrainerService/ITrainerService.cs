using PokemonContracts.DTOs;
using PokemonDomain.TrainerModel;

namespace PokemonApplication.TrainerService;
public interface ITrainerService
{
    Task CapturePokemon(int trainerId, int pokemonId);
    Task CreateTrainerAsync(string name, int age, string cpf);
    Task<TrainerDto?> GetById(int id);
    Task<List<CapturedPokemonDto>> GetCapturedPokemons(int id);
}
