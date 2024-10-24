using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonDomain.TrainerModel;

namespace PokemonInfrastructure.Persistence.Configurations;
internal class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Age)
            .IsRequired();

        builder.Property(t => t.CPF)
            .IsRequired()
            .HasMaxLength(11);

        builder.HasMany(t => t.Pokemons)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
