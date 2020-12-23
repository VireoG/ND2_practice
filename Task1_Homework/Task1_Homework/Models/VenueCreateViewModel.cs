using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business;

namespace Task1_Homework.Models
{
    public class VenueCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Adress { get; set; }

        public int CityId { get; set; }
    }
}
