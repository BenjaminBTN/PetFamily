using System.IO;

namespace PetFamily.Application.Providers.FileProvider.Dtos
{
    public record FileDto(Stream Stream, string BucketName, string ObjectName);
}
