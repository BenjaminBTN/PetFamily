using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain.Volunteers.VO;

namespace PetFamily.Application.Volunteers.Update
{
    public record UpdateMainInfoCommand(
        VolunteerId VolunteerId,
        FullNameDto FullNameDto,
        string Description,
        string Email,
        double Experience,
        string PhoneNumber);
}
