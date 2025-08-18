using ProvaPub.Application.DTOs;

namespace ProvaPub.Application.Interfaces
{
    public interface IPaginationService<T>
    {
        PaginatedList<T> GetPaginated(int page, int pageSize = 10);
    }
}
