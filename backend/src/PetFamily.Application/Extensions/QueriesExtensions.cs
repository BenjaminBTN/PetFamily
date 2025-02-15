using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            Page = page,
            PageSize = size
        };
    }
}

public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; set; } = [];
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public bool HasNextPage => Page * PageSize > TotalCount;
    public bool HasPreviousPage => Page > 1;
}