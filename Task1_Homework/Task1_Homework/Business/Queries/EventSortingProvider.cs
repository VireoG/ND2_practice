using System;
using System.Linq.Expressions;

namespace Task1_Homework.Business.Queries
{
    public class EventSortingProvider: BaseSortingProvider<Event>
    {
        protected override Expression<Func<Event, object>> GetSortExpression(BaseQuery query)
        {
            return query.SortBy switch
            {
                "Name" => p => p.Name,
                "City" => p => p.Venue.City.Id,
                "Venue" => p => p.Venue.Id,
                _ => p => p.Id
            };
        }
    }
}