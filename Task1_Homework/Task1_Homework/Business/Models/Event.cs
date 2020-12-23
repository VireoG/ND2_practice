using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;
using System.ComponentModel.DataAnnotations;

namespace Task1_Homework.Business
{
    public class Event : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int VenueId { get; set; }

        public Venue Venue { get; set; }

        public string Banner { get; set; }

        public string Description { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
