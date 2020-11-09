using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task1_Homework.Business;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Models;
using Task1_Homework.Models;

namespace Task1_Homework.Controllers
{
    public class TicketController : Controller
    {
        private readonly ResaleContext context;
        private readonly TicketService ticketService;
        private readonly EventService eventService;
        private readonly UserService userService;

        public TicketController(ResaleContext context)
        {
            this.context = context;
            ticketService = new TicketService(context);
            eventService = new EventService(context);
            userService = new UserService(context);
        }

        private async Task<List<Ticket>> GetTicketsListForIdentityUser()
        {
            var selected = from item in await ticketService.GetTickets()
                           where item.Seller.UserName != User.Identity.Name
                           select item;

            return selected.ToList();
        }

        [Authorize]
        public IActionResult CreateTicket([FromRoute] int id)
        {
            var model = new Ticket();
            var events = eventService.GetEventById(id).Result;
            model.Event = events;
            model.EventId = events.Id;
            var user = context.Users.SingleOrDefault(o => o.UserName == User.Identity.Name);
            model.Seller = user;
            model.SellerId1 = user.Id;
            return View("CreateTicket", model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket model)
        {

            if (model != null)
                await ticketService.Save(model);

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var listE = new SelectList(eventService.GetEvents().Result.ToArray(), "Id", "Name");
            ViewBag.Events = listE;
            var listU = new SelectList(context.Users.ToArray(), "Id", "UserName");
            ViewBag.Users = listU;
            var ticket = await ticketService.GetTicketById(id);
            return View("Edit", ticket);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Ticket model)
        {
            await ticketService.EditSave(model);
            return RedirectToAction("Index");
        }
    }
}
