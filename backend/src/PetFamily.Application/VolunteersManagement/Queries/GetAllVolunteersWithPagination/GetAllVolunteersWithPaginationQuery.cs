using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Queries.GetAllVolunteersWithPagination
{
    public record class GetAllVolunteersWithPaginationQuery(int PageNumber, int PageSize) : IQuery;
}
