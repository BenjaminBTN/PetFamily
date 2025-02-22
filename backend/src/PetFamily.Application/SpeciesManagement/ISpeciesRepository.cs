using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement;
using PetFamily.Domain.SpeciesManagement.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.SpeciesManagement
{
    public interface ISpeciesRepository
    {
        Task<Guid> Add(Species species, CancellationToken cancellationToken);
        Task Delete(Species species, CancellationToken cancellationToken);

        Task<Result<Species, Error>> GetById(SpeciesId speciesId, CancellationToken cancellationToken);
        Task<Result<Species, Error>> GetByName(SpeciesName name, CancellationToken cancellationToken);
    }
}
