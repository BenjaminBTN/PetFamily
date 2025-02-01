using PetFamily.Application.VolunteersManagement.Dtos;
using System;

namespace PetFamily.Application.VolunteersManagement.AddPet
{
    public record AddPetCommand(
        Guid Id,
        string Name,
        string Description,
        PetTypeDto TypeInfo,
        string Color,
        string HealthInfo,
        AddressDto Address,
        double Weight,
        double Height,
        string PhoneNumber,
        bool IsCastrated,
        bool IsVaccinated,
        string? BirthDate,
        int Status);
}
