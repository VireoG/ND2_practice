using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task1_Homework.Business.Models;
using Task1_Homework.Models;

namespace Task1_Homework.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketList ticketList;

        public TicketController(TicketList ticketList)
        {
            this.ticketList = ticketList;
        }
        public IActionResult Index()
        {
            var model = new TicketsViewModel
            {
                Tickets = ticketList.GetTicket()
            };

            return View(model);
        }
    }
}
