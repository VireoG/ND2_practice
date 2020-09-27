using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Models;
using Task1_Homework.Business;
using Task1_Homework.Business.Models;
using Microsoft.Extensions.Localization;

namespace Task1_Homework.Controllers
{
    public class EventController : Controller
    {
        private readonly EventList eventList;
        public readonly TicketList ticketList;
        public EventController(EventList eventList)
        { 
            ticketList = new TicketList();
            this.eventList = eventList;
        }
        public IActionResult Index()
        {
            var model = new EventsViewModel
            {
                Events = eventList.GetEvent()
            };

            return View(model);
        }

        public IActionResult Buy([FromRoute] int id)
        {
            var events = eventList.GetEventById(id);

            events.Tickets = Confirm(id);
            return View("Buy", events);
        }

        public List<Ticket> Confirm(int id)
        {
            var selected = from ticket in ticketList.tickets
                           where ticket.EventId == id
                           select ticket;

            return selected.ToList();
        }


    }
}
