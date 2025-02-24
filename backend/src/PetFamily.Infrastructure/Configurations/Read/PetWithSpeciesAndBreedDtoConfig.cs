using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Configurations.Read;

public class PetWithSpeciesAndBreedDtoConfig : IEntityTypeConfiguration<PetWithSpeciesAndBreedDto>
{
    public void Configure(EntityTypeBuilder<PetWithSpeciesAndBreedDto> builder)
    {
        builder.ToTable("pets");

        builder.HasNoKey();

        builder.Property(p => p.SpeciesId)
            .HasColumnName("species_id");

        builder.Property(p => p.BreedId)
            .HasColumnName("breed_id");
    }
}
