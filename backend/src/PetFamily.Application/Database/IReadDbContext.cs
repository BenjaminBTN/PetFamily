using Microsoft.EntityFrameworkCore;
using PetFamily.Application.VolunteersManagement.Dtos;

namespace PetFamily.Application.Database
{
    public interface IReadDbContext
    {
        DbSet<VolunteerDto> Volunteers { get; }
        DbSet<PetDto> Species { get; }
    }
}
