using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Models;

namespace PetFamily.Application.Extensions;

public static class QueriesExtensions
{
    public static async Task<PagedList<T>> ToPagedList<T>(
        this IQueryable<T> source, int page, int size, CancellationToken ct)
    {
        var totalCount = await source.CountAsync(ct);

        var items = await source.Skip((page - 1) * size).Take(size).ToListAsync(ct);

        return new PagedList<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = page,
            PageSize = size
        };
    }
}
