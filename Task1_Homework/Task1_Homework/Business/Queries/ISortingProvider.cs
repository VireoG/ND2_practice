using System.Linq;

namespace Task1_Homework.Business.Queries
{
    public interface ISortingProvider<T>
    {
        IOrderedQueryable<T> ApplySorting(IQueryable<T> queryable, BaseQuery query);
    }
}