using System.IO;

namespace PetFamily.Application.Providers.Dtos
{
    public record FileDto(Stream Stream, string Bucket, string Path);
}
