using PokemonDomain.PokemonModel;

namespace PokemonDomain.TypeModel;
public class Type
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public List<Pokemon> Pokemons { get; private set; } = [];

    private Type(string name)
    {
        Name = name;
    }

    public static Type Create(string name)
    {
        return new Type(name);
    }

    public Type()
    {
        
    }
}
