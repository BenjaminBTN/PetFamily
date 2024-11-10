using PetFamily.Domain.Volunteers;

namespace PetFamily.Application.Volunteers
{
    public interface IVolunteersRepository
    {
        public Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);
    }
}