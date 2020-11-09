using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Task1_Homework.Business;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Models;

namespace Task1_Homework.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ResaleContext context;
        private readonly ILogger<AdminController> logger;
        private readonly EventService eventService;
        private readonly VenueService venueService;
        private readonly CityService cityService;
        private readonly TicketService ticketService;
        public AdminController(ResaleContext context, ILogger<AdminController> logger)
        {
            ticketService = new TicketService(context);
            eventService = new EventService(context);
            cityService = new CityService(context);
            venueService = new VenueService(context);
            this.context = context;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult CreateEvent()
        {
            var list = new SelectList(venueService.GetVenues(), "Id", "Name");
            ViewBag.Venues = list;
            return View("CreateEvent");
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event model)
        {
            if (model != null)
                await eventService.Save(model);

            return RedirectToAction("Index");
        }

        public IActionResult CreateCity()
        {
            return View("CreateCity");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(City model)
        {
            if (model != null)
                await cityService.Save(model);

            return RedirectToAction("Index");
        }

        public IActionResult CreateVenue()
        {
            var list = new SelectList(cityService.GetCities(), "Id", "Name");
            ViewBag.Cities = list;
            return View("CreateVenue");
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenue(Venue model)
        {
            if(model != null)
                await venueService.Save(model);

            return RedirectToAction("Index");
        }
    }
}
