using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.VolunteersManagement.Commands.Create;

public record CreateVolunteerCommand(
    FullNameDto FullNameDto,
    string Description,
    string Email,
    double Experience,
    string PhoneNumber) : ICommand;
