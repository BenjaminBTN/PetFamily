using System;

namespace PetFamily.Application.VolunteersManagement.SoftDelete
{
    public record SoftDeleteVolunteerCommand(Guid VolunteerId);
}
