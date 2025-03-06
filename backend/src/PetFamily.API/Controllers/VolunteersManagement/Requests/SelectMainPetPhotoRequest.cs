using PetFamily.Application.VolunteersManagement.Commands.SelectMainPetPhoto;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests;

public record class SelectMainPetPhotoRequest(string FilePath)
{
    public SelectMainPetPhotoCommand ToCommand(Guid id, Guid petId) =>
        new SelectMainPetPhotoCommand(id, petId, FilePath);
}
