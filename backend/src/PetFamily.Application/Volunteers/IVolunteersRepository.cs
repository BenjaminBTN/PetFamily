using PetFamily.Domain.Volunteers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers
{
    public interface IVolunteersRepository
    {
        public Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);
    }
}