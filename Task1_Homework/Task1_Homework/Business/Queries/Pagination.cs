using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Queries
{
    public static class Pagination
    {
        public static PagedData<T> PagedResult<T>(PagedData<T> pagedData) where T : class
        {   
            var result = new PagedData<T>();
            result.Data = pagedData.Data.Skip(pagedData.PageSize * (pagedData.CurrentPage - 1)).Take(pagedData.PageSize).ToList();
            result.TotalNumber = pagedData.Data.Count();
            result.CurrentPage = pagedData.CurrentPage;
            return result;
        }
    }
}
