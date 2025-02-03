using PetFamily.Application.VolunteersManagement.DeleteFiles;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record DeletePetPhotosRequest(List<string> ObjectName, string BucketName)
    {
        public DeleteFilesCommand ToCommand(Guid volunteerId, Guid petId) =>
            new DeleteFilesCommand(volunteerId, petId, ObjectName, BucketName);
    }
}
