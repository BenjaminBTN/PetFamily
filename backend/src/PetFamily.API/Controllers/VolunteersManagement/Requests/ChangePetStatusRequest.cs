using PetFamily.Application.VolunteersManagement.Commands.ChangePetStatus;
using PetFamily.Domain.VolunteersManagement.Enums;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests;

public record class ChangePetStatusRequest(int Status)
{
    public ChangePetStatusCommand ToCommand(Guid volunteerId, Guid petId) => new(volunteerId, petId, Status);
}
