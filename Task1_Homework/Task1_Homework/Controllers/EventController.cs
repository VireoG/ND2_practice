using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Models;
using Task1_Homework.Business;
using Task1_Homework.Business.Models;
using Microsoft.Extensions.Localization;
using Task1_Homework.Business.Database;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Task1_Homework.Business.Services.IServices;

namespace Task1_Homework.Controllers
{
    public class EventController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly IEventService eventService;
        private readonly IVenueService venueService;

        public EventController(ITicketService ticketService, IEventService eventService, IVenueService venueService)
        {
            this.ticketService = ticketService;
            this.eventService = eventService;
            this.venueService = venueService;
        }

        public IActionResult Index()
        {
            var model = new EventsViewModel
            {
                Events = eventService.GetEvents().Result.ToArray()
            };

            return View(model);
        }

        public async Task<IActionResult> Buy([FromRoute] int? id)
        {
            if (id != null)
            {
                var events = await eventService.GetEventById(id);

                var tickets = await ticketService.GetTicketsByEventIdForIdentityUser(id , User.Identity.Name);

                events.Tickets = tickets;

                return View("Buy", events);
            }

            return NotFound();
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var list = new SelectList(venueService.GetVenues(), "Id", "Name");
                ViewBag.Venues = list;
                var ev = await eventService.GetEventById(id);
                return View(ev);
            }
            return NotFound();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Event model)
        {
            if (ModelState.IsValid)
            {
                await eventService.EditSave(model);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Edit");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Event eventt = await eventService.GetEventById(id);
                return PartialView("_Delete", eventt);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var ev = await eventService.GetEventById(id);
                if (ev != null)
                {
                    await eventService.Delete(ev);
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
