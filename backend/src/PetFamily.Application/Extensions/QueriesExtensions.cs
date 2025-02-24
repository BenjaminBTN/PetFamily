using System;
using System.Linq;
using System.Linq.Expressions;
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

        if (page <= 0)
            page = 1;

        if (size <= 0)
            size = 8;

        var items = await source.Skip((page - 1) * size).Take(size).ToListAsync(ct);

        return new PagedList<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = page,
            PageSize = size
        };
    }

    public static IQueryable<T> WhereIf<T>(this IQueryable<T> values, object? item, Expression<Func<T, bool>> expression)
    {
        if (item is null
        || (item is int i) && i == 0
        || (item is string s) && string.IsNullOrWhiteSpace(s))
            return values;

        return values.Where(expression);
    }
}
