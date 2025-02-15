using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.VolunteersManagement.Dtos;

namespace PetFamily.Infrastructure.Configurations.Read;

public class VolunteerDtoConfig : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volonteers");

        builder.HasKey(v => v.Id);

        builder.ComplexProperty(v => v.FullName, fnb =>
        {
            fnb.Property(fn => fn.Name).HasColumnName("name");
            fnb.Property(fn => fn.Surname).HasColumnName("surname");
            fnb.Property(fn => fn.Patronymic).HasColumnName("patronymic");
        });

        builder.Property(v => v.Description);

        builder.Property(v => v.Email);

        builder.Property(v => v.Experience);

        builder.Property(v => v.PhoneNumber);
    }
}
