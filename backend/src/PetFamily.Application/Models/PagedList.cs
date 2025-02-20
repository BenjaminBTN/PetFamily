using System.Collections.Generic;

namespace PetFamily.Application.Models;

public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; set; } = [];
    public long TotalCount { get; init; } = 1;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 8;
    public bool HasNextPage => PageNumber * PageSize < TotalCount;
    public bool HasPreviousPage => PageNumber > 1;
}