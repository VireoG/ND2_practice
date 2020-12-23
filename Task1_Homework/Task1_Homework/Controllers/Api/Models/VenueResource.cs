using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Controllers.Api.Models
{
    public class VenueResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public int CityId { get; set; }

    }
}
