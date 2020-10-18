using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task1_Homework.Business;
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
            var model = new TicketsViewModel();

            model.Tickets = GetTicketsListForIdentityUser().ToArray();
        
            return View(model);
        }

        private List<Ticket> GetTicketsListForIdentityUser()
        {
            var selected = from item in ticketList.GetTicket()
                           where item.Seller.FirstName != User.Identity.Name
                           select item;

            return selected.ToList();
        }
    }
}
