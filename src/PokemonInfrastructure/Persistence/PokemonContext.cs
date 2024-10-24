using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PokemonContracts.Options;
using PokemonDomain.PokemonModel;
using PokemonDomain.TrainerModel;
using PokemonInfrastructure.Persistence.Configurations;

namespace PokemonInfrastructure.Persistence;
public class PokemonContext : DbContext
{
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonDomain.TypeModel.Type> Types { get; set; }

    private readonly string _connectionString;

    public PokemonContext(DbContextOptions<PokemonContext> options, 
        IOptions<DatabaseSettings> dbSettings) : base(options)
    {
        _connectionString = dbSettings.Value.DefaultConnection;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TrainerConfiguration());
        modelBuilder.ApplyConfiguration(new PokemonConfiguration());
        modelBuilder.ApplyConfiguration(new TypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
