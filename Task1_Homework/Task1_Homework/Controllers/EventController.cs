﻿using Microsoft.AspNetCore.Mvc;
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

namespace Task1_Homework.Controllers
{
    public class EventController : Controller
    {
        private readonly ResaleContext context;
        private readonly ILogger<EventController> logger;
        private readonly EventService eventService;
        private readonly VenueService venueService;
        private readonly TicketService ticketService;
        public EventController(ResaleContext context, ILogger<EventController> logger)
        {
            ticketService = new TicketService(context);
            eventService = new EventService(context);
            venueService = new VenueService(context);
            this.context = context;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var model = new EventsViewModel
            {
                Events = eventService.GetEvents().Result.ToArray()
            };

            return View(model);
        }

        public async Task<IActionResult> Buy([FromRoute] int id)
        {
            var events = eventService.GetEventById(id).Result;
    
            var selected = from ticket in await ticketService.GetTickets()
                           where ticket.EventId == id && ticket.Seller.UserName != User.Identity.Name
                           select ticket;
            events.Tickets = selected.ToArray();

            return View("Buy", events);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var list = new SelectList(venueService.GetVenues(), "Id", "Name");
            ViewBag.Venues = list;
            var ev = await eventService.GetEventById(id);
            return View(ev);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Event model)
        {
            await eventService.EditSave(model);
            return RedirectToAction("Index");
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
