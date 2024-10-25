namespace PokemonContracts.DTOs;
public class PokemonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Evolutions { get; set; } = [];
    public List<string> Involutions { get; set; } = [];
    public List<string> Types { get; set; } = [];
}


