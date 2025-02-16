using System.Collections.Generic;

namespace PetFamily.Application.Models;

public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; set; } = [];
    public int TotalCount { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public bool HasNextPage => PageNumber * PageSize < TotalCount;
    public bool HasPreviousPage => PageNumber > 1;
}