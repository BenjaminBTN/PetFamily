using CSharpFunctionalExtensions;
using PetFamily.Domain.PetSpecies;
using PetFamily.Domain.Shared;
using System;

namespace PetFamily.Domain.Volunteers
{
    public record PetInfo
    {
        public SpeciesId SpeciesId { get; }
        public Guid BreedId { get; }

        private PetInfo(SpeciesId speciesId, Guid breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public static Result<PetInfo, Error> Create(SpeciesId speciesId, Guid breedId)
        {
            if(speciesId == null)
                return Errors.General.InvalidValue("SpeciesId");

            return new PetInfo(speciesId, breedId);
        }
    }
}