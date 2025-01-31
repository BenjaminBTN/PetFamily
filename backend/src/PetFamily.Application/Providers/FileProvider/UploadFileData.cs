using PetFamily.Domain.VolunteersManagement.VO;
using System.IO;

namespace PetFamily.Application.Providers.FileProvider
{
    public record class UploadFileData(Stream Stream, FilePath ObjectName);
}
