using PetFamily.Application.Providers.FileProvider;
using System;
using System.Collections.Generic;

namespace PetFamily.Application.VolunteersManagement.AddPetPhotos
{
    public record AddPetPhotosCommand(
        Guid VolunteerId,
        Guid PetId,
        IEnumerable<UploadFileData> Files,
        string BucketName);
}
