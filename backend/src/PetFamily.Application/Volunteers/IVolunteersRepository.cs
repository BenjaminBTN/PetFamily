using PetFamily.Domain.PetManagement;

namespace PetFamily.Application.Volunteers
{
    public interface IVolunteersRepository
    {
        public Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);
    }
}