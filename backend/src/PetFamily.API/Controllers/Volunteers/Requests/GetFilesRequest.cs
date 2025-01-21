using PetFamily.Application.Volunteers.GetFiles;

namespace PetFamily.API.Controllers.Volunteers.Requests
{
    public record GetFilesRequest(string ObjectName, string BucketName)
    {
        public GetFilesCommand ToCommand() =>
            new GetFilesCommand(ObjectName, BucketName);
    }
}
