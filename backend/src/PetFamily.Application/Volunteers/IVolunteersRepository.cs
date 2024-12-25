using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.VO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers
{
    public interface IVolunteersRepository
    {
        Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);
        Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken);
        Task<Guid> Delete(Guid volunteerId, CancellationToken cancellationToken);

        // могу ли пойти таким путем, и обработать ошибку(null) уже в хендлере?
        Task<Result<Volunteer, ErrorList>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken);
    }
}