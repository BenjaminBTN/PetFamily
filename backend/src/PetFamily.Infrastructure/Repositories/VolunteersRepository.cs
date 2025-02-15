using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.VolunteersManagement;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement;
using PetFamily.Domain.VolunteersManagement.VO;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories
{
    public class VolunteersRepository : IVolunteersRepository
    {
        private readonly WriteDbContext _context;
        private readonly ILogger<VolunteersRepository> _logger;

        public VolunteersRepository(WriteDbContext context, ILogger<VolunteersRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            await _context.Volunteers.AddAsync(volunteer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return volunteer.Id.Value;
        }


        public async Task<Guid> SoftDelete(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            volunteer.Delete();
            await _context.SaveChangesAsync(cancellationToken);
            return volunteer.Id.Value;
        }


        public async Task<Guid> Restore(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            volunteer.Restore();
            await _context.SaveChangesAsync(cancellationToken);
            return volunteer.Id.Value;
        }


        public async Task<Guid> HardDelete(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            _context.Volunteers.Remove(volunteer);
            await _context.SaveChangesAsync(cancellationToken);
            return volunteer.Id.Value;
        }


        public async Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default)
        {
            var volunteer = await _context.Volunteers
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

            if(volunteer == null)
            {
                _logger.LogError("The volunteer record with ID '{id}' was not found",
                    volunteerId.Value);
                return Errors.General.NotFound(volunteerId.Value);
            }

            return volunteer;
        }
    }
}
