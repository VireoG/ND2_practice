using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business;

namespace Task1_Homework.Models
{
    public class OrderCreateViewModel
    {
        public int TicketId { get; set; }
        public string BuyerId { get; set; }
    }
}
