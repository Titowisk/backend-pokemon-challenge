using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonDomain.PokemonModel;
using PokemonDomain.TrainerModel.Entities;

namespace PokemonInfrastructure.Persistence.Configurations;
public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Weight)
            .IsRequired();

        builder.Property(p => p.Height)
            .IsRequired();

        builder.Property(p => p.Base64Sprite)
            .IsRequired();

        builder.HasMany(p => p.Types)
            .WithMany(t => t.Pokemons);

        builder.HasMany(p => p.Evolutions)
            .WithMany(p => p.Involutions)
            .UsingEntity<PokemonEvolution>()
            ;

        //builder.Navigation(p => p.Types).AutoInclude();
        //builder.Navigation(p => p.Evolutions).AutoInclude();
    }
}
