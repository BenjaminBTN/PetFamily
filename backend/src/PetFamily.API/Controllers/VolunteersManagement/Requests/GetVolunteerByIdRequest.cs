using PetFamily.Application.VolunteersManagement.Queries.GetVolunteerById;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests;

public record class GetVolunteerByIdRequest(Guid VolunteerId)
{
    public GetVolunteerByIdQuery ToQuery() => new (VolunteerId);
}
