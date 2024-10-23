using PokemonDomain.TrainerModel.Entities;
using System.Collections.Generic;
using _Type = PokemonDomain.TypeModel.Type;

namespace PokemonDomain.PokemonModel;
public class Pokemon
{
    public IReadOnlyList<_Type> Types { get; private set; } = [];
    public IReadOnlyList<Pokemon> Evolutions { get; private set; } = [];
    public IReadOnlyList<Pokemon> Involutions { get; private set; } = [];

    public int Id { get; private set; }
    public string Name { get; private set; }
    public float Weight { get; private set; }
    public float Height { get; private set; }
    public string Base64Sprite { get; private set; }

    private Pokemon(string name, float weight, float height, string base64Sprite)
    {
        Name = name;
        Weight = weight;
        Height = height;
        Base64Sprite = base64Sprite;
    }

    public static Pokemon Create(string name, float weight, float height, string base64Sprite)
    {
        return new Pokemon(name, weight, height, base64Sprite);
    }
}
