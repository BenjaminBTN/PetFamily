using CSharpFunctionalExtensions;
using PetFamily.Domain.PetSpecies;

namespace PetFamily.Domain.Volunteers
{
    public record PetInfo
    {
        private PetInfo(SpeciesId speciesId, Guid breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public SpeciesId SpeciesId { get; }
        public Guid BreedId { get; }

        public static Result<PetInfo> Create(SpeciesId speciesId, Guid breedId)
        {
            if(speciesId == null)
                return Result.Failure<PetInfo>("SpeciesId can not be null");

            return new PetInfo(speciesId, breedId);
        }
    }
}
