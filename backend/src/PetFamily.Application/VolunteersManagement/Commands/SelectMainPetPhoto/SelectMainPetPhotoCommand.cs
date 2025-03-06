using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Commands.SelectMainPetPhoto;

public record class SelectMainPetPhotoCommand(Guid Id, Guid PetId, string FilePath) : ICommand;
