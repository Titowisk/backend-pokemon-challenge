using PokemonDomain.PokemonModel;
using PokemonDomain.TrainerModel;

namespace PokemonApplication.Persistence;
public interface ITrainerRepository
{
    Task<bool> CpfExistsAsync(string cpf);
    Task<Trainer> CreateTrainerAsync(string name, int age, string cpf);
    Task<Trainer?> GetById(int id);
    Task<List<Pokemon>> GetCapturedPokemons(int id);
    Task UpdateTrainerAsync(Trainer trainer);
}
