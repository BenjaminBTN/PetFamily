using System;

namespace PetFamily.Application.SpeciesManagement.AddBreed
{
    public record AddBreedCommand(Guid SpeciesId, string Name);
}
