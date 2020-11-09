using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;

namespace Task1_Homework.Business
{
    public class City : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public ICollection<Venue> Venues { get; set; }
    }
}
