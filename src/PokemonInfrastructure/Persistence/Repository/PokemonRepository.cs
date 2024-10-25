using Microsoft.EntityFrameworkCore;
using PokemonApplication.Persistence;
using PokemonDomain.PokemonModel;

namespace PokemonInfrastructure.Persistence.Repository;
public class PokemonRepository : IPokemonRepository
{
    private readonly PokemonContext _context;

    public PokemonRepository(PokemonContext context)
    {
        _context = context;
    }

    public async Task<List<Pokemon>> GetAll()
    {
        return await _context.Pokemons.ToListAsync();
    }

    public async Task<Pokemon?> GetById(int id)
    {
        return await _context
            .Pokemons
                .Include(x => x.Evolutions)
                .Include(x => x.Involutions)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
