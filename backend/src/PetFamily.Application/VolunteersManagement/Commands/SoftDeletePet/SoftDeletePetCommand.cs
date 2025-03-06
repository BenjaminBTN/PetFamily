using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Commands.SoftDeletePet;

public record class SoftDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
