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

        public Event Event { get; set; }

        public decimal Price { get; set; }

        public int SellerId { get; set; }

        public User Seller { get; set; }

        public TicketSaleStatus Status { get; set; }
    }
}
