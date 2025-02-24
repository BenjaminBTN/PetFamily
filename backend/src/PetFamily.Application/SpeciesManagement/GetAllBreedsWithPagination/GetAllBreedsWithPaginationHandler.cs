using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;

namespace PetFamily.Application.SpeciesManagement.GetAllBreedsWithPagination;

public class GetAllBreedsWithPaginationHandler
    : IQueryHandler<PagedList<BreedDto>, GetAllBreedsWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetAllBreedsWithPaginationHandler> _logger;

    public GetAllBreedsWithPaginationHandler(IReadDbContext readDbContext, ILogger<GetAllBreedsWithPaginationHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PagedList<BreedDto>> Handle(GetAllBreedsWithPaginationQuery query, CancellationToken ct)
    {
        var breedsQuery = _readDbContext.Breeds;

        breedsQuery = breedsQuery
            .Where(b => b.SpeciesId == query.SpeciesId)
            .WhereIf(query.BreedId, b => b.Id == query.BreedId)
            .WhereIf(query.Name, b => b.Name.Contains(query.Name!));

        _logger.LogInformation("The requestes collection of items 'Breeds' has been successfully compiled");

        return await breedsQuery.ToPagedList(query.PageNumber, query.PageSize, ct);
    }
}
