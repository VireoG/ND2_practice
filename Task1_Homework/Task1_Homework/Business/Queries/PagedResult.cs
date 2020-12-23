using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Queries
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public ICollection<T> Items { get; set; }
    }

}
