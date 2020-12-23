using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;

namespace Task1_Homework.Business.Models
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public TicketSaleStatus Status { get; set; }

        public string BuyerId { get; set; }

        public User Buyer { get; set; }

        public string TrackNumber { get; set; }
    }
}
