using System;
using System.Linq;
using System.Linq.Expressions;

namespace Task1_Homework.Business.Queries
{
    public abstract class BaseSortingProvider<T> : ISortingProvider<T>
    {
        public IOrderedQueryable<T> ApplySorting(IQueryable<T> queryable, PagedData<Event> pagedData)
        {
            var sortExpression = GetSortExpression(pagedData);

            return pagedData.SortOrder switch
            {
                SortOrder.Descending => queryable.OrderByDescending(sortExpression),
                _ => queryable.OrderBy(sortExpression)
            };
        }

        protected abstract Expression<Func<T, object>> GetSortExpression(PagedData<Event> pagedData);
    }
}