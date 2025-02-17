using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteersManagement.Queries.GetAllVolunteersWithPagination
{
    public record class GetAllVolunteersWithPaginationQuery(
        int PageNumber,
        int PageSize,
        Guid? Id,
        string? Name,
        int? PageFrom,
        int? PageTo) : IQuery;
}
