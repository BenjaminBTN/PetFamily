using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.SpeciesManagement;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesManagement;
using PetFamily.Domain.SpeciesManagement.VO;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _context;
    private readonly ILogger<SpeciesRepository> _logger;

    public SpeciesRepository(WriteDbContext context, ILogger<SpeciesRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> Add(Species species, CancellationToken cancellationToken)
    {
        await _context.Species.AddAsync(species, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return species.Id.Value;
    }

    public async Task Delete(Species species, CancellationToken cancellationToken)
    {
        _context.Species.Remove(species);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result<Species, Error>> GetById(SpeciesId speciesId, CancellationToken cancellationToken)
    {
        var species = await _context.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Id == speciesId, cancellationToken);

        if(species == null)
        {
            _logger.LogError("The species record with the ID '{id}' was not found",
                speciesId.Value);
            return Errors.General.NotFound(speciesId.Value);
        }

        return species;
    }

    public async Task<Result<Species, Error>> GetByName(SpeciesName name, CancellationToken cancellationToken)
    {
        var species = await _context.Species
            .Include(s => s.Breeds)
            .FirstOrDefaultAsync(s => s.Name == name, cancellationToken);

        if(species == null)
        {
            _logger.LogError("The species record with the name '{name}' was not found", 
                name.Value);
            return Errors.General.NotFound("Species Name");
        }

        return species;
    }
}
