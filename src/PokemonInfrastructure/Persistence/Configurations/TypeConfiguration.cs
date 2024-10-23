using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonInfrastructure.Persistence.Configurations;

public class TypeConfiguration : IEntityTypeConfiguration<PokemonDomain.TypeModel.Type>
{
    public void Configure(EntityTypeBuilder<PokemonDomain.TypeModel.Type> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(t => t.Pokemons)
            .WithMany(p => p.Types);
    }
}
