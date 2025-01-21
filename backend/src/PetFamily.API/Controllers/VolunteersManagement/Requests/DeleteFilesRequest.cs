using PetFamily.Application.VolunteersManagement.DeleteFiles;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record DeleteFilesRequest(string ObjectName, string BucketName)
    {
        public DeleteFilesCommand ToCommand() =>
            new DeleteFilesCommand(ObjectName, BucketName);
    }
}
