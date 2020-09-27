using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Models
{
    public class TicketList
    {
        public List<Ticket> tickets = new List<Ticket>();
        private EventList eventList;

        public TicketList()
        {
            eventList = new EventList();

            tickets.AddRange(
                 new[]
                 {
                     new Ticket { Id = 1, EventId = 7, Price = 100, Seller = "User1", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 2, EventId = 2, Price = 110, Seller = "User2", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 3, EventId = 1, Price = 110, Seller = "User1", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 4, EventId = 2, Price = 120, Seller = "User3", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 5, EventId = 8, Price = 120, Seller = "User2", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 6, EventId = 9, Price = 220, Seller = "User1", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 7, EventId = 3, Price = 130, Seller = "User2", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 8, EventId = 4, Price = 130, Seller = "User3", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 9, EventId = 5, Price = 180, Seller = "User1", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 10, EventId = 4, Price = 110, Seller = "User2", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 11, EventId = 1, Price = 360, Seller = "User1", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 12, EventId = 5, Price = 180, Seller = "User3", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 13, EventId = 9, Price = 206, Seller = "User2", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 14, EventId = 4, Price = 120, Seller = "User1", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 15, EventId = 5, Price = 160, Seller = "User2", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 16, EventId = 10, Price = 230, Seller = "User3", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 17, EventId = 6, Price = 100, Seller = "User1", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 18, EventId = 7, Price = 610, Seller = "User2", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 19, EventId = 6, Price = 410, Seller = "User1", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 20, EventId = 9, Price = 120, Seller = "User3", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 21, EventId = 10, Price = 120, Seller = "User2", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 22, EventId = 8, Price = 420, Seller = "User1", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 23, EventId = 8, Price = 130, Seller = "User2", Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 24, EventId = 10, Price = 730, Seller = "User3", Status = TicketSaleStatus.Sale },
                 });

            GetEventName();
        }

        public void GetEventName()
        {
            foreach (var ev in eventList.events)
            {
                foreach (var ticket in tickets)
                {
                    if (ticket.EventId == ev.Id)
                    {
                        ticket.EventName = ev.Name;
                    }
                }
            }
        }

        public Ticket[] GetTicket()
        {
       
            return tickets.ToArray();
        }
    }
}
