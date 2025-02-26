using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.Queries.GetAllSpeciesWithPagination;

public record class GetAllSpeciesWithPaginationQuery(
    int PageNumber,
    int PageSize,
    Guid? Id,
    string? Name,
    int? PageFrom,
    int? PageTo) : IQuery;
