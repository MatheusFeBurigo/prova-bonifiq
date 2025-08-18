using ProvaPub.Application.DTOs;

public static class PaginationHelper
{
    public static PaginatedList<T> Paginate<T>(IQueryable<T> query, int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        var items = query.Skip(skip).Take(pageSize).ToList();
        var totalCount = query.Count();
        var hasNext = (skip + pageSize) < totalCount;

        return new PaginatedList<T>
        {
            Items = items,
            TotalCount = totalCount,
            HasNext = hasNext
        };
    }
}