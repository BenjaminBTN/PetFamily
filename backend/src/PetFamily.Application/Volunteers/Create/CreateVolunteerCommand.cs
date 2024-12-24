using PetFamily.Application.Volunteers.Dtos;

namespace PetFamily.Application.Volunteers.Create
{
    public record CreateVolunteerCommand(
        FullNameDto FullNameDto,
        string Description,
        string Email,
        double Experience,
        string PhoneNumber);
}
