using PetFamily.Application.VolunteersManagement.Queries.GetAllVolunteersWithPagination;

namespace PetFamily.API.Controllers.VolunteersManagement.Requests;

public record class GetAllVolunteersWithPaginationRequest(
    int PageNumber,
    int PageSize,
    Guid? Id,
    string? Name,
    int? PageFrom,
    int? PageTo)
{
    public GetAllVolunteersWithPaginationQuery ToQuery() =>
        new (PageNumber, PageSize, Id, Name, PageFrom, PageTo);
}
