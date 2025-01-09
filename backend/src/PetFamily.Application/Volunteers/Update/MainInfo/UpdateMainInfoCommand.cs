using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.Volunteers.Update.MainInfo
{
    public record UpdateMainInfoCommand(
        VolunteerId VolunteerId,
        FullNameDto FullNameDto,
        string Description,
        string Email,
        double Experience,
        string PhoneNumber);
}
