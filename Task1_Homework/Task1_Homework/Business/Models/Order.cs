using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business
{
    public class Order
    {
        public string Id { get; set; }

        public Ticket Ticket { get; set; }

        public TicketSaleStatus Status { get; set; }    

        public User Buyer { get; set; }

        public string TrackNumber { get; set; }
    }
}
