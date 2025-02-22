using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.DeleteBreed;

public record class DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;
