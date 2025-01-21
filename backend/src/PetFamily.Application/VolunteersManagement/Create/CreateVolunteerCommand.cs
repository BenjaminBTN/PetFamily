using PetFamily.Application.VolunteersManagement.Dtos;

namespace PetFamily.Application.VolunteersManagement.Create
{
    public record CreateVolunteerCommand(
        FullNameDto FullNameDto,
        string Description,
        string Email,
        double Experience,
        string PhoneNumber);
}
