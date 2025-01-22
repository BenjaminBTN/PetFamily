using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.SpeciesManagement;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement;
using PetFamily.Domain.SpeciesManagement.VO;

namespace PetFamily.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly ApplicationDbContext _context;

        public SpeciesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Species species, CancellationToken cancellationToken)
        {
            await _context.Species.AddAsync(species, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return species.Id.Value;
        }

        public async Task<Guid> Delete(Species species, CancellationToken cancellationToken)
        {
            _context.Species.Remove(species);
            await _context.SaveChangesAsync(cancellationToken);
            return species.Id.Value;
        }

        public async Task<Result<Species, Error>> GetById(SpeciesId speciesId, CancellationToken cancellationToken)
        {
            var species = await _context.Species
                .Include(s => s.Breeds)
                .FirstOrDefaultAsync(s => s.Id == speciesId, cancellationToken);

            if(species == null)
                return Errors.General.NotFound(speciesId.Value);

            return species;
        }

        public async Task<Guid> Save(Species species, CancellationToken cancellationToken)
        {
            _context.Species.Attach(species);
            await _context.SaveChangesAsync(cancellationToken);
            return species.Id.Value;
        }
    }
}
