using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Models
{
    public class CityCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
