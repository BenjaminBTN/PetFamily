using PetFamily.Application.Volunteers.DeleteFiles;

namespace PetFamily.API.Controllers.Volunteers.Requests
{
    public record DeleteFilesRequest(string ObjectName, string BucketName)
    {
        public DeleteFilesCommand ToCommand() =>
            new DeleteFilesCommand(ObjectName, BucketName);
    }
}
