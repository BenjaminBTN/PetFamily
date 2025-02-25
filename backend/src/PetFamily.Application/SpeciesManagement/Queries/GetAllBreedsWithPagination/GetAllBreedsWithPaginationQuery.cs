using System;
using PetFamily.Application.Abstractions;

namespace PetFamily.Application.SpeciesManagement.GetAllBreedsWithPagination;

public record class GetAllBreedsWithPaginationQuery(
    Guid SpeciesId,
    int PageNumber,
    int PageSize,
    Guid? BreedId,
    string? Name,
    int? PageFrom,
    int? PageTo) : IQuery;
