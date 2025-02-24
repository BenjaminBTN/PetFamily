using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;

namespace PetFamily.Application.VolunteersManagement.Queries.GetAllVolunteersWithPagination;

public class GetAllVolunteersWithPaginationHandler : 
    IQueryHandler<PagedList<VolunteerDto>, GetAllVolunteersWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<GetAllVolunteersWithPaginationHandler> _logger;

    public GetAllVolunteersWithPaginationHandler(
        IReadDbContext readDbContext, ILogger<GetAllVolunteersWithPaginationHandler> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }

    public async Task<PagedList<VolunteerDto>> Handle(
        GetAllVolunteersWithPaginationQuery query,
        CancellationToken ct)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        volunteersQuery = volunteersQuery
            .WhereIf(query.Id, v => v.Id == query.Id)
            .WhereIf(query.Name, v => v.FullName.Name.Contains(query.Name!)
                                   || v.FullName.Surname.Contains(query.Name!));

        _logger.LogInformation("The requestes collection of items 'Species' has been successfully compiled");

        return await volunteersQuery.ToPagedList(query.PageNumber, query.PageSize, ct);
    }
}
