using System.Threading;
using System.Threading.Tasks;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Application.VolunteersManagement.Dtos;

namespace PetFamily.Application.VolunteersManagement.Queries.GetAllVolunteersWithPagination;

public class GetAllVolunteersWithPaginationHandler
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
        var volunteersQuery = _readDbContext.Volunteers.AsQueryable();

        return await volunteersQuery.ToPagedList(query.Page, query.Size, ct);
    }
}
