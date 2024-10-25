using PokemonDomain.PokemonModel;
using System.Text.RegularExpressions;

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
        if (!IsValidCpf(cpf))
            throw new ArgumentException("Invalid CPF");

        if(age <= 0)
            throw new ArgumentException("Invalid age");

        if(string.IsNullOrEmpty(name))
            throw new ArgumentException("Invalid name");

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

    private static bool IsValidCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
            return false;

        var tempCpf = cpf.Substring(0, 9);
        var firstDigit = CalculateCpfDigit(tempCpf);
        var secondDigit = CalculateCpfDigit(tempCpf + firstDigit);

        return cpf.EndsWith(firstDigit.ToString() + secondDigit.ToString());
    }

    private static int CalculateCpfDigit(string cpf)
    {
        var sum = 0;
        for (int i = 0; i < cpf.Length; i++)
        {
            sum += int.Parse(cpf[i].ToString()) * (cpf.Length + 1 - i);
        }

        var remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }

    public Trainer()
    {
    }
}
