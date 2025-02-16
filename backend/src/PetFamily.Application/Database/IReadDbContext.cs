using System.Linq;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.Database
{
    public interface IReadDbContext
    {
        IQueryable<VolunteerDto> Volunteers { get; }
        IQueryable<PetDto> Species { get; }
    }
}
