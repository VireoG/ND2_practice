using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Models
{
    public class TicketList
    {
        private readonly List<Ticket> tickets = new List<Ticket>();
        private readonly EventList eventList;
        private readonly UserList userList;

        public TicketList(EventList eventList, UserList userList)
        {
            this.eventList = eventList;
            this.userList = userList;

            tickets.AddRange(
                 new[]
                 {
                     new Ticket { Id = 1,EventId = 1, Price = 100, SellerId = 1, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 2, EventId = 2, Price = 110, SellerId = 2, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 3, EventId = 1, Price = 110, SellerId = 1, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 4, EventId = 2, Price = 120, SellerId = 3, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 5, EventId = 8, Price = 120, SellerId = 2, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 6, EventId = 9, Price = 220, SellerId = 1, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 7, EventId = 3, Price = 130, SellerId = 2, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 8, EventId = 4, Price = 130, SellerId = 3, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 9, EventId = 5, Price = 180, SellerId= 1, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 10, EventId = 4, Price = 110, SellerId = 2, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 11, EventId = 1, Price = 360, SellerId= 1, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 12, EventId = 5, Price = 180, SellerId = 3, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 13, EventId = 9, Price = 206, SellerId = 2, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 14, EventId = 4, Price = 120, SellerId = 1, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 15, EventId = 5, Price = 160, SellerId = 2, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 16, EventId = 10, Price = 230, SellerId = 3, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 17, EventId = 6, Price = 100, SellerId = 1, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 18, EventId = 7, Price = 610, SellerId = 2, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 19, EventId = 6, Price = 410, SellerId= 1, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 20, EventId = 9, Price = 120, SellerId = 3, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 21, EventId = 10, Price = 120, SellerId = 2, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 22, EventId = 8, Price = 420, SellerId = 1, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 23, EventId = 8, Price = 130, SellerId = 2, Status = TicketSaleStatus.Sale },
                     new Ticket { Id = 24, EventId = 10, Price = 730, SellerId= 3, Status = TicketSaleStatus.Sale },
                 });

            GetFieldsById();
        }

        private void GetFieldsById()
        {
            foreach (var ticket in tickets)
            {
                ticket.Event = eventList.GetEventById(ticket.EventId);
                ticket.Seller = userList.GetUserById(ticket.SellerId);
            }     
        }

        public Ticket[] GetTicket()
        {      
            return tickets.ToArray();
        }

        public Ticket GetTicketById(int id)
        {
            return tickets.FirstOrDefault(t => t.Id == id);
        }
    }
}
