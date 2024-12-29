using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.Species.VO;
using PetFamily.Domain.Volunteers.Entities;
using PetFamily.Domain.Volunteers.VO;

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

            builder.OwnsOne(p => p.PhoneNumber, phb =>
            {
                phb.Property(phb => phb.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_PHONE_NUMBER_LENGTH)
                .HasColumnName("phone_number");
            });

            builder.Property(p => p.IsCastrated)
                .IsRequired();

            builder.Property(p => p.IsVaccinated)
                .IsRequired();

            builder.Property(p => p.BirthDate)
                .IsRequired(false);

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>();


            builder.OwnsOne(p => p.RequisitesForHelp, rhb =>
            {
                rhb.ToJson("requisites_for_help");
                rhb.OwnsMany(r => r.Requisites, rb =>
                {
                    rb.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    rb.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
                });
            });


            builder.OwnsOne(p => p.PetPhotos, ppb =>
            {
                ppb.ToJson("pet_photos");
                ppb.OwnsMany(p => p.Photos, pb =>
                {
                    pb.Property(r => r.PathToStorage)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

                    pb.Property(r => r.IsMain)
                    .IsRequired();
                });
            });


            builder.Property(p => p.CreationDate)
                .IsRequired();

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
