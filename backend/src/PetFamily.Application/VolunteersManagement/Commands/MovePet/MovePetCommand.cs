using System;

namespace PetFamily.Application.VolunteersManagement.Commands.MovePet
{
    public record class MovePetCommand(Guid VolunteerId, Guid PetId, int NewPosition);
}
