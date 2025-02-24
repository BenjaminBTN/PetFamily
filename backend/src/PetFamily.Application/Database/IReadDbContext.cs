using System.Linq;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.Database
{
    public interface IReadDbContext
    {
        IQueryable<VolunteerDto> Volunteers { get; }
        IQueryable<PetDto> Pets { get; }
        IQueryable<PetWithSpeciesAndBreedDto> PetsWithSpeciesAndBreed { get; }
        IQueryable<SpeciesDto> Species { get; }
        IQueryable<BreedDto> Breeds { get; }
    }
}
