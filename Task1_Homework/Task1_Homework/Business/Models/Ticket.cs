using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;

namespace Task1_Homework.Business
{
   

    public class Ticket : IEntity
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public decimal Price { get; set; }
        
        public string SellerId { get; set; }
        public User Seller { get; set; }
        public TicketSaleStatus Status { get; set; }
    }
}
