namespace PokemonApplication.TrainerService;
public interface ITrainerService
{
    Task CreateTrainerAsync(string name, int age, string cpf);
}
