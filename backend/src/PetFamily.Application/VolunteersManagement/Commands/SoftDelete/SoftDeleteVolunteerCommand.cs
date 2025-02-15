using System;

namespace PetFamily.Application.VolunteersManagement.Commands.SoftDelete
{
    public record SoftDeleteVolunteerCommand(Guid VolunteerId);
}
