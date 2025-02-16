using System;
using System.Collections.Generic;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Commands.DeleteFiles
{
    public record DeleteFilesCommand(
        Guid VolunteerId,
        Guid PetId,
        IEnumerable<string> ObjectNames,
        string BucketName) : ICommand;
}
