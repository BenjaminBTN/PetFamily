using PetFamily.Application.SpeciesManagement.GetAllBreedsWithPagination;

namespace PetFamily.API.Controllers.SpeciesManagement.Requests;

public record class GetAllBreedsWithPaginationRequest(
    int PageNumber,
    int PageSize,
    Guid? BreedId,
    string? Name,
    int? PageFrom,
    int? PageTo)
{
    public GetAllBreedsWithPaginationQuery ToQuery(Guid speciesId) =>
        new(speciesId, PageNumber, PageSize, BreedId, Name, PageFrom, PageTo);
}
