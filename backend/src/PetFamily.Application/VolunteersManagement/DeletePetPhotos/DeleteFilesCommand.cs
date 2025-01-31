using System;
using System.Collections.Generic;

namespace PetFamily.Application.VolunteersManagement.DeletePetPhotos
{
    public record DeletePetPhotosCommand(
        Guid VolunteerId,
        Guid PetId,
        IEnumerable<string> ObjectNames, 
        string BucketName);
}
