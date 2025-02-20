using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;
using System;

namespace PetFamily.Application.VolunteersManagement.Commands.AddPet
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
        int Status) : ICommand;
}
