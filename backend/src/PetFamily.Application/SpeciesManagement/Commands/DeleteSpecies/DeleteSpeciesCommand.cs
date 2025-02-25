using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.Commands.DeleteSpecies;

public record class DeleteSpeciesCommand(Guid Id) : ICommand;
