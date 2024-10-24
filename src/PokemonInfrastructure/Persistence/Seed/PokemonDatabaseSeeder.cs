using PokemonDomain.PokemonModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PokemonInfrastructure.Persistence.Seed
{
    public class PokemonDatabaseSeeder
    {
        private readonly HttpClient _httpClient;
        private readonly PokemonContext _context;

        public PokemonDatabaseSeeder(HttpClient httpClient, PokemonContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task SeedTable()
        {
            _context.Database.EnsureCreated();
            await SeedPokemonTableAsync();
            await SeedTypeTableAsync();
        }

        public async Task SeedPokemonTableAsync()
        {
            int howManyPokemons = 10;
            for (int pokeIndex = 1; pokeIndex <= howManyPokemons; pokeIndex++)
            {
                Task.Delay(200).Wait(); // Delay to avoid rate limiting

                var r = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{pokeIndex}");
                PokemonReference? pokemonReference = JsonSerializer.Deserialize<PokemonReference>(r);

                if (pokemonReference != null)
                {
                    var pokemon = Pokemon.Create(
                        pokemonReference.Name,
                        pokemonReference.Weight / 10.0f, // Convert to kg
                        pokemonReference.Height / 10.0f, // Convert to meters
                        pokemonReference.Sprites.FrontDefault
                    );

                    // Code to save the pokemon to the database
                    // Example: _dbContext.Pokemons.Add(pokemon);
                    // await _dbContext.SaveChangesAsync();
                    _context.Pokemons.Add(pokemon);
                    await _context.SaveChangesAsync();
                    // add evolutions
                }
            }
        }

        public async Task SeedTypeTableAsync()
        {
            var response = await _httpClient.GetStringAsync("https://pokeapi.co/api/v2/type");
            var typeList = JsonSerializer.Deserialize<TypeListReference>(response);

            // Code to seed the Type table using typeList
        }
    }

    #region Pokemon Reference
    public class PokemonReference
    {
        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; }

        [JsonPropertyName("types")]
        public List<TypeSlot> Types { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }
    }

    public class Sprites
    {
        [JsonPropertyName("back_default")]
        public string BackDefault { get; set; }

        [JsonPropertyName("back_female")]
        public string BackFemale { get; set; }

        [JsonPropertyName("back_shiny")]
        public string BackShiny { get; set; }

        [JsonPropertyName("back_shiny_female")]
        public string BackShinyFemale { get; set; }

        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }

        [JsonPropertyName("front_female")]
        public string FrontFemale { get; set; }

        [JsonPropertyName("front_shiny")]
        public string FrontShiny { get; set; }

        [JsonPropertyName("front_shiny_female")]
        public string FrontShinyFemale { get; set; }
    }

    public class TypeSlot
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public TypeInfo Type { get; set; }
    }

    public class TypeInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
    #endregion

    #region Pokemon Type
    public class TypeListReference
    {
        public List<Type> Results { get; set; }
    }

    public class Type
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
    #endregion
}
