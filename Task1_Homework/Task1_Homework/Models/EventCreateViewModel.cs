using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Models
{
    public class EventCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int VenueId { get; set; }
        [Required]
        public string Banner { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
