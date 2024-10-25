namespace PokemonContracts.DTOs;

public class TrainerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string CPF { get; set; } = string.Empty;
    public List<PokemonDto> CapturedPokemons { get; set; } = [];
}
