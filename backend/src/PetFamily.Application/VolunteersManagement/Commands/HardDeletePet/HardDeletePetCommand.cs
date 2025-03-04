using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Commands.HardDeletePet;

public record class HardDeletePetCommand(Guid VolunteerId, Guid PetId) : ICommand;
