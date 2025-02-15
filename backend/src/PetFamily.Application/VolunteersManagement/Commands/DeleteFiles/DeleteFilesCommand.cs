using System;
using System.Collections.Generic;

namespace PetFamily.Application.VolunteersManagement.Commands.DeleteFiles
{
    public record DeleteFilesCommand(
        Guid VolunteerId,
        Guid PetId,
        IEnumerable<string> ObjectNames,
        string BucketName);
}
