using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement.Entities;
using PetFamily.Domain.SpeciesManagement.VO;

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

            builder.ComplexProperty(b => b.Name, nb =>
            {
                nb.Property(n => n.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("name");
            });
        }
    }
}
