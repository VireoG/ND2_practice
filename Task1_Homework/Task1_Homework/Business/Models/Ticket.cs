using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business
{
    public class Ticket
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public string EventName { get; set; }

        public decimal Price { get; set; }

        public string Seller { get; set; }

        public TicketSaleStatus Status { get; set; }
    }
}
