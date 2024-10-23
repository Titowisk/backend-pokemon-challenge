using PokemonDomain.PokemonModel;

namespace PokemonDomain.TypeModel;
public class Type
{
    public string Name { get; set; }
    public List<Pokemon> Pokemons { get; set; } = [];

    private Type(string name)
    {
        Name = name;
    }

    public static Type Create(string name)
    {
        return new Type(name);
    }
}
