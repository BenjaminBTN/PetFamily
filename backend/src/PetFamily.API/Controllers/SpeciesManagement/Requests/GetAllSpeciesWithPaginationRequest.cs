using PetFamily.Application.SpeciesManagement.Queries.GetAllSpeciesWithPagination;

namespace PetFamily.API.Controllers.SpeciesManagement.Requests;

public record class GetAllSpeciesWithPaginationRequest(
    int PageNumber,
    int PageSize,
    Guid? Id,
    string? Name,
    int? PageFrom,
    int? PageTo)
{
    public GetAllSpeciesWithPaginationQuery ToQuery() =>
        new(PageNumber, PageSize, Id, Name, PageFrom, PageTo);
}
