using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonDomain.PokemonModel;

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
            .WithOne()
            .HasForeignKey(e => e.PreviousEvolutionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(p => p.Types).AutoInclude();
        builder.Navigation(p => p.Evolutions).AutoInclude();
    }
}
