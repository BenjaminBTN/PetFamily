using System;

namespace PetFamily.Application.VolunteersManagement.MovePet
{
    public record class MovePetCommand(Guid VolunteerId, Guid PetId, int NewPosition);
}
