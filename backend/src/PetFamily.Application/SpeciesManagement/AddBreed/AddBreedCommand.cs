using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.AddBreed
{
    public record AddBreedCommand(Guid SpeciesId, string Name) : ICommand;
}
