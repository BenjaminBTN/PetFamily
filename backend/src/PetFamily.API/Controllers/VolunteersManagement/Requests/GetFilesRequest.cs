using PetFamily.Application.VolunteersManagement.GetFiles;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests
{
    public record GetFilesRequest(string ObjectName, string BucketName)
    {
        public GetFilesCommand ToCommand() =>
            new GetFilesCommand(ObjectName, BucketName);
    }
}
