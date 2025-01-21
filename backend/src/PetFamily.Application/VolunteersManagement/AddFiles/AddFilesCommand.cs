using System.IO;

namespace PetFamily.Application.VolunteersManagement.AddFiles
{
    public record AddFilesCommand(Stream Stream, string BucketName, string ObjectName);
}
