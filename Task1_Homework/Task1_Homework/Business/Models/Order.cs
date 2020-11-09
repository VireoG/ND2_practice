using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;

namespace Task1_Homework.Business
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        public Ticket Ticket { get; set; }

        public TicketSaleStatus Status { get; set; }    

        public User Buyer { get; set; }

        public string TrackNumber { get; set; }
    }
}
