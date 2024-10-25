using PokemonApplication.Persistence;
using PokemonContracts.DTOs;
using PokemonDomain.PokemonModel;

namespace PokemonApplication.TrainerService;
public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IPokemonRepository _pokemonRepository;

    public TrainerService(ITrainerRepository trainerRepository, IPokemonRepository pokemonRepository)
    {
        _trainerRepository = trainerRepository;
        _pokemonRepository = pokemonRepository;
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

    public async Task CapturePokemon(int trainerId, int pokemonId)
    {
        var trainer = await _trainerRepository.GetById(trainerId)
            ?? throw new InvalidOperationException("Trainer not found");

        var pokemon = await _pokemonRepository.GetById(pokemonId)
            ?? throw new InvalidOperationException("Pokemon not found");

        trainer.AddPokemon(pokemon);
        await _trainerRepository.UpdateTrainerAsync(trainer);
    }

    public async Task<TrainerDto?> GetById(int id)
    {
        var trainer = await _trainerRepository.GetById(id)
            ?? throw new InvalidOperationException("Trainer not found");

        var trainerDto = new TrainerDto
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Age = trainer.Age,
            CPF = trainer.CPF,
            CapturedPokemons = trainer.Pokemons.Select(p => new PokemonDto
            {
                Id = p.Id,
                Name = p.Name,
                Evolutions = p.Evolutions.Select(e => e.Name).ToList(),
                Involutions = p.Involutions.Select(i => i.Name).ToList(),
                Types = p.Types.Select(t => t.Name).ToList()
            }).ToList()
        };

        return trainerDto;
    }

    public async Task<List<CapturedPokemonDto>> GetCapturedPokemons(int id)
    {
        List<Pokemon> pokemons = await _trainerRepository.GetCapturedPokemons(id);

        var pokemonDtos = pokemons.Select(p => new CapturedPokemonDto
        {
            Id = p.Id,
            Name = p.Name,
            Types = p.Types.Select(t => t.Name).ToList()
        }).ToList();

        return pokemonDtos;
    }
}
