using PetFamily.Domain.VolunteersManagement.VO;

namespace PetFamily.Application.Volunteers.SoftDelete
{
    public record SoftDeleteVolunteerCommand(VolunteerId VolunteerId);
}
