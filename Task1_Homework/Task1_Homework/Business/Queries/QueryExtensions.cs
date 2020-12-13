using System.Linq;

namespace Task1_Homework.Business.Queries
{
    public static class QueryExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> queryable, PagedData<T> pagedData)
        {
            if (pagedData.TotalPages <= 0)
                pagedData.TotalPages = 10;

            if (pagedData.CurrentPage <= 0)
                pagedData.CurrentPage = 1;

            return queryable.Skip(pagedData.TotalPages * (pagedData.CurrentPage - 1)).Take(pagedData.TotalPages);
        }
    }
}