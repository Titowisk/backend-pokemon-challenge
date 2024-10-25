namespace PokemonContracts.DTOs;
public class CapturedPokemonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Types { get; set; } = [];
}
