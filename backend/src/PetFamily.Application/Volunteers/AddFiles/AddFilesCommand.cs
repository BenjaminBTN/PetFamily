using System.IO;

namespace PetFamily.Application.Volunteers.AddFiles
{
    public record AddFilesCommand(Stream Stream, string BucketName, string ObjectName);
}
