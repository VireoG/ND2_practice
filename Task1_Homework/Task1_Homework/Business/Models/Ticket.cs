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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Event Event { get; set; }

        public decimal Price { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SellerId1 { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SellerName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public User Seller { get; set; }

        public TicketSaleStatus Status { get; set; }
    }
}
