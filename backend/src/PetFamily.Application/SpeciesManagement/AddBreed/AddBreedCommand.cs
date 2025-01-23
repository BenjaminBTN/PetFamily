using PetFamily.Domain.SpeciesManagement.VO;

namespace PetFamily.Application.SpeciesManagement.AddBreed
{
    public record AddBreedCommand(SpeciesId SpeciesId, string Name);
}
