using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;

namespace PetFamily.Application.SpeciesManagement.Queries.GetAllSpeciesWithPagination;

public class GetAllSpeciesWithPaginationHandler
    : IQueryHandler<PagedList<SpeciesDto>, GetAllSpeciesWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetAllSpeciesWithPaginationHandler> _logger;

    public GetAllSpeciesWithPaginationHandler(IReadDbContext readDbContext, ILogger<GetAllSpeciesWithPaginationHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PagedList<SpeciesDto>> Handle(GetAllSpeciesWithPaginationQuery query, CancellationToken ct)
    {
        var speciesQuery = _readDbContext.Species;

        speciesQuery = speciesQuery
            .WhereIf(query.Id, s => s.Id == query.Id)
            .WhereIf(query.Name, s => s.Name.Contains(query.Name!.ToLower()));

        _logger.LogInformation("The requestes collection of items 'Species' has been successfully compiled");

        return await speciesQuery.ToPagedList(query.PageNumber, query.PageSize, ct);
    }
}
