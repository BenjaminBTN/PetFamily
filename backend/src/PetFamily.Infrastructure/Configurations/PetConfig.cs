using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.PetSpecies;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Configurations
{
    public class PetConfig : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                id => id.Value,
                value => PetId.Create(value));

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.ComplexProperty(p => p.Info, ib =>
            {
                ib.Property(i => i.SpeciesId)
                .IsRequired()
                .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value))
                .HasColumnName("species_id");

                ib.Property(i => i.BreedId)
                .IsRequired()
                .HasColumnName("breed_id");
            });

            builder.Property(p => p.Color)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(p => p.HealthInfo)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);


            builder.ComplexProperty(p => p.Address, ab => 
            {
                ab.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("country");

                ab.Property(a => a.Region)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("region");

                ab.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("city");

                ab.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("street");

                ab.Property(a => a.HouseNumber)
                .IsRequired()
                .HasColumnName("house_number");

                ab.Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(Address.POSTAL_CODE_LENGTH)
                .HasColumnName("postal_code");
            });


            builder.Property(p => p.Weight)
                .IsRequired();

            builder.Property(p => p.Height)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(Constants.MAX_PHONE_TEXT_LENGTH);

            builder.Property(p => p.IsCastrated)
                .IsRequired();

            builder.Property(p => p.IsVaccinated)
                .IsRequired();

            builder.Property(p => p.BirthDate);

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.CreationDate)
                .IsRequired();

            builder.OwnsOne(p => p.Details, db => 
            {
                db.ToJson();

                db.OwnsMany(d => d.RequisitesForHelp, rb => 
                {
                    rb.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                    rb.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
                });

                db.OwnsMany(d => d.PetPhotos, pb => 
                {
                    pb.Property(p => p.PathToStorage)
                    .IsRequired();
                    pb.Property(p => p.IsMain)
                    .IsRequired();
                });
            });
        }
    }
}
