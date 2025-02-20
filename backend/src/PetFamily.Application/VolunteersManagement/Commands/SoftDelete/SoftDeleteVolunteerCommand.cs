using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Commands.SoftDelete
{
    public record SoftDeleteVolunteerCommand(Guid VolunteerId) : ICommand;
}
