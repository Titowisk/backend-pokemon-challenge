using PokemonDomain.PokemonModel;
using PokemonDomain.TrainerModel;

namespace PokemonApplication.TrainerService;
public interface ITrainerService
{
    Task CapturePokemon(int trainerId, int pokemonId);
    Task CreateTrainerAsync(string name, int age, string cpf);
    Task<Trainer?> GetById(int id);
    Task<List<Pokemon>> GetCapturedPokemons(int id);
}
