using PokemonContracts.DTOs;

namespace PokemonApplication.PokemonService
{
    public interface IPokemonService
    {
        Task<PokemonDto> GetById(int id);
        Task<List<PokemonDto>> GetRandomPokemonsAsync(int quantity);
    }
}
