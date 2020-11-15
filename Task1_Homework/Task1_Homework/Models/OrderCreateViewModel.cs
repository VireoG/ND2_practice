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
        [Required]
        public string TrackNumber { get; set; }
        public string EventName { get; set; }
        public int TicketId { get; set; }
        public decimal TicketPrice { get; set; }
        public string BuyerName { get; set; }
        public string BuyerId { get; set; }
    }
}
