namespace PokemonContracts.Options;
public class DatabaseSettings
{
    public const string DatabaseSettingsKey = "ConnectionStrings";
    public string DefaultConnection { get; set; }
}
