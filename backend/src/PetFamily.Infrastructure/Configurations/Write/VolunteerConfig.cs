using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Domain.Shared.VO;
using PetFamily.Domain.VolunteersManagement;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Infrastructure.Configurations.Write;

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
            .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
            .HasColumnName("name");

            fnb.Property(fn => fn.Surname)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
            .HasColumnName("surname");

            fnb.Property(fn => fn.Patronymic)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
            .HasColumnName("patronymic");
        });


        builder.ComplexProperty(v => v.Description, db =>
        {
            db.Property(d => d.Value)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_HIGH_TEXT_LENGTH)
            .HasColumnName("description");
        });

        builder.ComplexProperty(v => v.Email, eb =>
        {
            eb.Property(e => e.Value)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
            .HasColumnName("email");
        });

        builder.Property(v => v.Experience)
            .IsRequired();

        builder.ComplexProperty(v => v.PhoneNumber, pb =>
        {
            pb.Property(p => p.Value)
            .IsRequired()
            .HasMaxLength(Domain.Shared.Constants.MAX_PHONE_NUMBER_LENGTH)
            .HasColumnName("phone_number");
        });


        builder.Property(v => v.Requisites)
            .HasConversion(
                requisites => JsonSerializer
                    .Serialize(requisites
                        .Select(r => new VolunteerRequsiteDto(r.Name, r.Description)),
                    JsonSerializerOptions.Default),

                json => JsonSerializer
                    .Deserialize<IEnumerable<VolunteerRequsiteDto>>(json, JsonSerializerOptions.Default)!
                    .Select(dto => VolunteerRequisite.Create(dto.Name, dto.Description).Value).ToList());
                    

        builder.Property(v => v.Networks)
            .HasConversion(
                networks => JsonSerializer
                    .Serialize(networks
                        .Select(n => new SocialNetworkDto(n.Name, n.Url)),
                    JsonSerializerOptions.Default),

                json => JsonSerializer
                    .Deserialize<IEnumerable<SocialNetworkDto>>(json, JsonSerializerOptions.Default)!
                    .Select(dto => SocialNetwork.Create(dto.Name, dto.Url).Value).ToList());


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
