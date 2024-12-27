using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.VO;

namespace PetFamily.Infrastructure.Configurations
{
    public class VolunteerConfig : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volonteers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));


            builder.ComplexProperty(v => v.FullName, fnb =>
            {
                fnb.Property(fn => fn.Name)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("name");

                fnb.Property(fn => fn.Surname)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("surname");

                fnb.Property(fn => fn.Patronymic)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("patronymic");
            });


            builder.ComplexProperty(v => v.Description, db =>
            {
                db.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("description");
            });

            builder.ComplexProperty(v => v.Email, eb =>
            {
                eb.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("email");
            });

            builder.Property(v => v.Experience)
                .IsRequired();

            builder.ComplexProperty(v => v.PhoneNumber, pb =>
            {
                pb.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_PHONE_NUMBER_LENGTH)
                .HasColumnName("phone_number");
            });


            builder.OwnsOne(v => v.Requisites, vrb =>
            {
                vrb.ToJson("requisites");

                vrb.OwnsMany(vr => vr.Requisites, rb =>
                {
                    rb.Property(r => r.Name)
                    .IsRequired(false)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    rb.Property(r => r.Description)
                    .IsRequired(false)
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
                });
            });


            builder.OwnsOne(v => v.Networks, vnb =>
            {
                vnb.ToJson("networks");

                vnb.OwnsMany(vn => vn.Networks, nb =>
                {
                    nb.Property(n => n.Name)
                    .IsRequired(false)
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                    nb.Property(n => n.Url)
                    .IsRequired(false);
                });
            });


            builder.Property(v => v.CreationDate)
                .IsRequired();

            builder.HasMany(v => v.Pets)
                .WithOne()
                .HasForeignKey("volunteer_id");

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
