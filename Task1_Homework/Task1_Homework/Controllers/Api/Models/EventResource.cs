using System;
using System.Collections.Generic;
using Task1_Homework.Business;

namespace Task1_Homework.Controllers.Api.Models
{
    public class EventResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int VenueId { get; set; }
        public string Banner { get; set; }
        public string Description { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
