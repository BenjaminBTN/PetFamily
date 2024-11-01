using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.PetSpecies;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Configurations
{
    public class BreedConfig : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("breeds");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                b => b.Value,
                value => BreedId.Create(value));

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        }
    }
}
