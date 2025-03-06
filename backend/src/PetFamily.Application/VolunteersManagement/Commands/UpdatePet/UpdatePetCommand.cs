using System;
using System.Collections.Generic;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.VolunteersManagement.Commands.UpdatePet;

public record class UpdatePetCommand(
    Guid Id,
    Guid PetId,
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
    List<RequisiteForHelpDto> RequisitesForHelp) : ICommand;