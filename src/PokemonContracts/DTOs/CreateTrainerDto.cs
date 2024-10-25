using System.Text.Json.Serialization;

namespace PokemonContracts.DTOs;
public class CreateTrainerDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("cpf")]
    public string CPF { get; set; } = string.Empty;
}
