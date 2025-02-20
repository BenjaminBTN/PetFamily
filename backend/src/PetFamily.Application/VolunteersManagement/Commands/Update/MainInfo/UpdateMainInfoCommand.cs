using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;
using System;

namespace PetFamily.Application.VolunteersManagement.Commands.Update.MainInfo
{
    public record UpdateMainInfoCommand(
        Guid VolunteerId,
        FullNameDto FullNameDto,
        string Description,
        string Email,
        double Experience,
        string PhoneNumber) : ICommand;
}
