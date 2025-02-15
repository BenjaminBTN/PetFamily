using PetFamily.Application.VolunteersManagement.Commands.MovePet;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record class MovePetRequest(Guid PetId, int NewPosition)
    {
        public MovePetCommand ToCommand(Guid volunteerId) =>
            new(volunteerId, PetId, NewPosition);
    }
}
