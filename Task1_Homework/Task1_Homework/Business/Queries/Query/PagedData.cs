using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Queries
{
    public class PagedData<T> : BaseQuery
    {
        public string Search { get; set; }
        public List<T> Data { get; set; }
        public int[] Cities { get; set; }
        public int[] Venues { get; set; }
    }
}
