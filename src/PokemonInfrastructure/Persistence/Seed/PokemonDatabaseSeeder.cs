using Microsoft.EntityFrameworkCore;
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
            await SeedTypeTableAsync();
            await SeedPokemonTableAsync();
        }

        public async Task SeedTypeTableAsync()
        {
            if (_context.Types.Any())
            {
                return;
            }

            var response = await _httpClient.GetStringAsync("https://pokeapi.co/api/v2/type");
            var typeList = JsonSerializer.Deserialize<TypeListReference>(response);

            if (typeList != null)
            {
                foreach (var type in typeList.Results)
                {
                    var newType = PokemonDomain.TypeModel.Type.Create(type.Name);
                    _context.Types.Add(newType);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task SeedPokemonTableAsync()
        {
            if(_context.Pokemons.Any())
            {
                return;
            }

            int howManyPokemons = 9;
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
                        pokemonReference.Height * 10.0f, // Convert to cm
                        pokemonReference.Sprites.FrontDefault
                    );

                    SetPokemonType(pokemon, pokemonReference.Types);

                    _context.Pokemons.Add(pokemon);
                   
                    await _context.SaveChangesAsync();
                }
            }

            await SeedPokemonEvolutionTableAsync();
        }

        private void SetPokemonType(Pokemon pokemon, List<TypeSlot> types)
        {
            foreach (var item in types)
            {
                PokemonDomain.TypeModel.Type? databaseType = _context
                    .Types
                    .FirstOrDefault(t => t.Name == item.Type.Name);

                if (databaseType is not null)
                {
                    pokemon.Types.Add(databaseType);
                }
            }
        }

        /// <summary>
        /// https://stackoverflow.com/questions/69783976/how-to-fetch-the-data-of-evolution-chain-from-the-pokeapi
        /// </summary>
        /// <returns></returns>
        public async Task SeedPokemonEvolutionTableAsync()
        {
            List<Pokemon> allPokemons = _context.Pokemons
                .Include(x => x.Evolutions)
                .Include(x => x.Involutions)
                .ToList();

            foreach (var pokemon in allPokemons)
            {
                Task.Delay(200).Wait(); // Delay to avoid rate limiting

                var pokemonResponse = await _httpClient.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{pokemon.Id}");
                PokemonReference? pokemonReference = JsonSerializer.Deserialize<PokemonReference>(pokemonResponse);

                var speciesResponse = await _httpClient.GetStringAsync(pokemonReference.Species.Url);
                SpeciesPrincipal? speciesPrincipal = JsonSerializer.Deserialize<SpeciesPrincipal>(speciesResponse);

                var evolutionChain = await _httpClient.GetStringAsync(speciesPrincipal.EvolutionChain.Url);
                EvolutionChainPrincipal? evolutionChainPrincipal = JsonSerializer.Deserialize<EvolutionChainPrincipal>(evolutionChain);

                if (evolutionChainPrincipal is not null && evolutionChainPrincipal.Chain.EvolvesTo.Count != 0) 
                {
                    Species? nextEvolutionSpecies = GetNextEvolution(evolutionChainPrincipal.Chain, pokemonReference.Species.Url);
                    if (nextEvolutionSpecies is null)
                    {
                        continue; // Pokemon has no evolution
                    }

                    Pokemon? evolution = allPokemons
                        .FirstOrDefault(p => p.Name == nextEvolutionSpecies.Name);

                    if(evolution is null)
                    {
                        continue; // Pokemon not found in database
                    }

                    pokemon.Evolutions.Add(evolution);
                    evolution.Involutions.Add(pokemon);


                    await _context.SaveChangesAsync();
                }
            }
        }

        private static Species? GetNextEvolution(Chain chain, string speciesUrl)
        {
            if (chain.Species.Url == speciesUrl)
            {
                if (chain.EvolvesTo.Count > 0)
                {
                    return chain.EvolvesTo[0].Species;
                }
                return null;
            }

            foreach (var evolvesTo in chain.EvolvesTo)
            {
                var result = GetNextEvolution(evolvesTo, speciesUrl);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
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

        [JsonPropertyName("species")]
        public Species Species { get; set; }

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

    public class Species
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
        [JsonPropertyName("results")]
        public List<Type> Results { get; set; }
    }

    public class Type
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
    #endregion

    #region Pokemon Species Reference
    public class EvolutionChain
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class SpeciesPrincipal
    {
        [JsonPropertyName("evolution_chain")]
        public EvolutionChain EvolutionChain { get; set; }
    }
    #endregion

    #region Evolution Reference
    public class EvolutionChainPrincipal
    {
        [JsonPropertyName("chain")]
        public Chain Chain { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

    public class Chain
    {
        [JsonPropertyName("evolves_to")]
        public List<Chain> EvolvesTo { get; set; }

        [JsonPropertyName("is_baby")]
        public bool IsBaby { get; set; }

        [JsonPropertyName("species")]
        public Species Species { get; set; }
    }

    #endregion
}
