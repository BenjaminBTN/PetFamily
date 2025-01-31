using PetFamily.Application.VolunteersManagement.DeletePetPhotos;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record DeletePetPhotosRequest(List<string> ObjectName, string BucketName)
    {
        public DeletePetPhotosCommand ToCommand(Guid volunteerId, Guid petId) =>
            new DeletePetPhotosCommand(volunteerId, petId, ObjectName, BucketName);
    }
}
