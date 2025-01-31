using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.SpeciesManagement.VO;
using PetFamily.Domain.VolunteersManagement.Entities;
using PetFamily.Domain.VolunteersManagement.VO;

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

            builder.ComplexProperty(p => p.Name, db =>
            {
                db.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("name");
            });

            builder.ComplexProperty(p => p.Description, db =>
            {
                db.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("despription");
            });


            builder.ComplexProperty(p => p.TypeInfo, ib =>
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

            builder.ComplexProperty(p => p.Color, db =>
            {
                db.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("color");
            });

            builder.ComplexProperty(p => p.HealthInfo, hb =>
            {
                hb.IsRequired();

                hb.Property(h => h.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("health_info");
            });


            builder.ComplexProperty(p => p.Address, ab => 
            {
                ab.IsRequired();

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

            builder.OwnsOne(p => p.PhoneNumber, pnb =>
            {
                pnb.Property(pn => pn.Value)
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

            builder.ComplexProperty(p => p.OrdinalNumber, ob =>
            {
                ob.Property(o => o.Value)
                .IsRequired()
                .HasColumnName("ordinal_number");
            });


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
                    .HasConversion(
                        p => p.Value,
                        value => FilePath.Create(value).Value)
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
