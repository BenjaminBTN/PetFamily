using PetFamily.Application.VolunteersManagement.Queries.GetAllVolunteersWithPagination;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests;

public record class GetAllVolunteersWithPaginationRequest(int Page, int Size)
{
    public GetAllVolunteersWithPaginationQuery ToQuery() =>
        new (Page, Size);
}
