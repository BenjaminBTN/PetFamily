using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.VO;

namespace PetFamily.Infrastructure.Repositories
{
    public class VolunteersRepository : IVolunteersRepository
    {
        private readonly ApplicationDbContext _context;

        public VolunteersRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            await _context.Volunteers.AddAsync(volunteer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return volunteer.Id.Value;
        }


        public async Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            // метод на всякий случай включает отслежвание сущности
            _context.Volunteers.Attach(volunteer);
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


        public async Task<Result<Volunteer, ErrorList>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default)
        {
            var volunteer = await _context.Volunteers
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

            if(volunteer == null)
                return Errors.General.NotFound(volunteerId.Value).ToErrorList();

            return volunteer;
        }
    }
}
