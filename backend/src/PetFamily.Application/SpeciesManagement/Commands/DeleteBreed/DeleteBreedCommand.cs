using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.Commands.DeleteBreed;

public record class DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;
