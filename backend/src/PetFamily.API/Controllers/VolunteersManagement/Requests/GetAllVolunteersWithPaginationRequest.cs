using PetFamily.Application.VolunteersManagement.Queries.GetAllVolunteersWithPagination;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests;

public record class GetAllVolunteersWithPaginationRequest(
    int Page,
    int PageSize,
    Guid? Id,
    string? Title,
    int? PageFrom,
    int? PageTo)
{
    public GetAllVolunteersWithPaginationQuery ToQuery() =>
        new (Page, PageSize, Id, Title, PageFrom, PageTo);
}
