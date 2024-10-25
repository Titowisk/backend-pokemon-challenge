using PokemonApplication.Persistence;

namespace PokemonApplication.TrainerService;
public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepository;

    public TrainerService(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }

    public async Task CreateTrainerAsync(string name, int age, string cpf)
    {
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (await _trainerRepository.CpfExistsAsync(cpf))
        {
            throw new InvalidOperationException("Trainer could not be created");
        }

        await _trainerRepository.CreateTrainerAsync(name, age, cpf);
    }
}
