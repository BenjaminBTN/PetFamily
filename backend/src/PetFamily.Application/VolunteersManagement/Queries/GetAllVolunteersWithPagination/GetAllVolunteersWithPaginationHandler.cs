using System.Threading;
using System.Threading.Tasks;
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

    public GetAllVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDto>> Handle(
        GetAllVolunteersWithPaginationQuery query,
        CancellationToken ct)
    {
        var volunteersQuery = _readDbContext.Volunteers;

        volunteersQuery = volunteersQuery.WhereIf(query.Id, t => t.Id == query.Id);

        return await volunteersQuery.ToPagedList(query.PageNumber, query.PageSize, ct);
    }
}
