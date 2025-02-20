using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Commands.HardDelete
{
    public record HardDeleteVolunteerCommand(Guid VolunteerId) : ICommand;
}
