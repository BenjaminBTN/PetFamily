using PetFamily.Application.VolunteersManagement.Dtos;
using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.VolunteersManagement.Update.MainInfo
{
    public record UpdateMainInfoCommand(
        VolunteerId VolunteerId,
        FullNameDto FullNameDto,
        string Description,
        string Email,
        double Experience,
        string PhoneNumber);
}
