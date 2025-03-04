using System;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.VolunteersManagement.Enums;

namespace PetFamily.Application.VolunteersManagement.Commands.ChangePetStatus;

public record class ChangePetStatusCommand(Guid VolunteerId, Guid PetId, int Status) : ICommand;
