using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business
{
    public class Venue
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }
    }
}
