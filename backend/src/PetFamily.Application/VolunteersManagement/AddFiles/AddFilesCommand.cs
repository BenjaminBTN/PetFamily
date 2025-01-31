using PetFamily.Application.VolunteersManagement.Dtos;
using System;
using System.Collections.Generic;

namespace PetFamily.Application.VolunteersManagement.AddFiles
{
    public record AddFilesCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Files, string BucketName);
}
