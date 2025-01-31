using PetFamily.Domain.VolunteersManagement.VO;
using System.IO;

namespace PetFamily.Application.VolunteersManagement.Dtos
{
    public record class UploadFileDto(Stream Stream, FilePath ObjectName);
}
