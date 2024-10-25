using Microsoft.EntityFrameworkCore;
using PokemonApplication.Persistence;
using PokemonDomain.TrainerModel;

namespace PokemonInfrastructure.Persistence.Repository;
public class TrainerRepository : ITrainerRepository
{
    private readonly PokemonContext _context;

    public TrainerRepository(PokemonContext pokemonContext)
    {
        _context = pokemonContext;
    }

    public async Task<Trainer> CreateTrainerAsync(string name, int age, string cpf)
    {
        var trainer = Trainer.Create(name, age, cpf);
        _context.Trainers.Add(trainer);
        await _context.SaveChangesAsync();
        return trainer;
    }

    public async Task<bool> CpfExistsAsync(string cpf)
    {
        return await _context.Trainers.AnyAsync(t => t.CPF == cpf);
    }
}
