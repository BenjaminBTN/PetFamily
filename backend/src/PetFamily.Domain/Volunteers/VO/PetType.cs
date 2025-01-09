using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species.VO;
using System;

namespace PetFamily.Domain.Volunteers.VO
{
    public record PetType
    {
        public SpeciesId SpeciesId { get; }
        public Guid BreedId { get; } = Guid.Empty;

        private PetType(SpeciesId speciesId, Guid breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public static Result<PetType, Error> Create(SpeciesId speciesId, Guid breedId)
        {
            if(speciesId == null)
                return Errors.General.InvalidValue("SpeciesId");

            return new PetType(speciesId, breedId);
        }
    }
}