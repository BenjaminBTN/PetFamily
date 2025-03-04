using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Commands.RestorePet;

public record class RestorePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
