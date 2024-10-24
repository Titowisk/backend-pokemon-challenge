using PokemonDomain.PokemonModel;

namespace PokemonDomain.TrainerModel;
public class Trainer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }
    public string CPF { get; private set; }
    public List<Pokemon> Pokemons { get; private set; } = [];

    private Trainer(string name, int age, string cpf)
    {
        Name = name;
        Age = age;
        CPF = cpf;
    }

    public static Trainer Create(string name, int age, string cpf)
    {
        return new Trainer(name, age, cpf);
    }

    public void AddPokemon(Pokemon pokemon)
    {
        Pokemons.Add(pokemon);
    }

    public void RemovePokemon(Pokemon pokemon)
    {
        Pokemons.Remove(pokemon);
    }

    public Trainer()
    {
    }
}
