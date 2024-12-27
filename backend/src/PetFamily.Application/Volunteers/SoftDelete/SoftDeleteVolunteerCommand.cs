using PetFamily.Domain.Volunteers.VO;

namespace PetFamily.Application.Volunteers.SoftDelete
{
    public record SoftDeleteVolunteerCommand(VolunteerId VolunteerId);
}
