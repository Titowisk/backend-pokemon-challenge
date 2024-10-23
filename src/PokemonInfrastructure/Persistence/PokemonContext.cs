using Microsoft.EntityFrameworkCore;
using PokemonDomain.PokemonModel;
using PokemonDomain.TrainerModel;
using PokemonInfrastructure.Persistence.Configurations;

namespace PokemonInfrastructure.Persistence;
public class PokemonContext : DbContext
{
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TrainerConfiguration());
        modelBuilder.ApplyConfiguration(new PokemonConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    // Add your DbContext options configuration here
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: SqLite
        // Example: optionsBuilder.UseSqlServer("YourConnectionString");
    }
}
