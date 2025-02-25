using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Commands.GetFiles;

public record GetFilesCommand(string ObjectName, string BucketName) : ICommand;
