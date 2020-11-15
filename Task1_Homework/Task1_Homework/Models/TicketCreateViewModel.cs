using System.ComponentModel.DataAnnotations;

namespace Task1_Homework.Models
{
    public class TicketCreateViewModel
    {
        public int EventId { get; set; }

        public string EventName { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}