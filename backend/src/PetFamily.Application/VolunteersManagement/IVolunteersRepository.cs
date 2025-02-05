using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.VolunteersManagement;
using PetFamily.Domain.VolunteersManagement.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteersManagement
{
    public interface IVolunteersRepository
    {
        Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);
        Task<Guid> SoftDelete(Volunteer volunteer, CancellationToken cancellationToken);
        Task<Guid> Restore(Volunteer volunteer, CancellationToken cancellationToken);
        Task<Guid> HardDelete(Volunteer volunteer, CancellationToken cancellationToken);

        Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken);
    }
}
