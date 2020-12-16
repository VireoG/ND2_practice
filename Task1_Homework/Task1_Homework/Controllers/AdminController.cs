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
using Task1_Homework.Business.Services.IServices;
using Task1_Homework.Models;

namespace Task1_Homework.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ICityService cityService;
        private readonly IEventService eventService;
        private readonly ITicketService ticketService;
        private readonly IVenueService venueService;

        public AdminController(ICityService cityService, IEventService eventService, ITicketService ticketService, IVenueService venueService)
        {
            this.cityService = cityService;
            this.eventService = eventService;
            this.ticketService = ticketService;
            this.venueService = venueService;
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult CreateEvent()
        {
            var venues = new SelectList(venueService.GetVenues(), "Id", "Name");
            ViewBag.Venues = venues;
            return View("CreateEvent");
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var @event = new Event
                {
                    Banner = model.Banner,
                    Date = model.Date,
                    Description = model.Description,
                    VenueId = model.VenueId,
                    Name = model.Name
                };
                await eventService.Save(@event);
                return RedirectToAction("Index");
            }
            return RedirectToAction("CreateEvent");
        }

        public IActionResult CreateCity()
        {
            return View("City/CreateCity");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(CityCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var city = new City
                {
                    Name = model.Name,
                    Country = model.Country
                };

                await cityService.Save(city);
                return RedirectToAction("City/GetCityList");
            }

            return RedirectToAction("City/CreateCity");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditCity(int id)
        {
            var city = await cityService.GetCityById(id);

            return View("City/EditCity",city);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> EditCity(City model)
        {
            await cityService.EditSave(model);
            return RedirectToAction("City/GetCityList");
        }

        [ActionName("DeleteCity")]
        public async Task<IActionResult> ConfirmDeleteCity(int? id)
        {
            if (id != null)
            {
                var city = await cityService.GetCityById(id);
                return PartialView("City/_DeleteCity", city);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id != null)
            {
                var city = await cityService.GetCityById(id);
                if (city != null)
                {
                    await cityService.Delete(city);
                    return RedirectToAction("City/GetCityList");
                }
            }
            return NotFound();
        }

        public IActionResult GetCityList()
        {
            var cvm = new CitiesViewModel
            {
                Cities = cityService.GetCities().ToArray()
            };

            return View("City/GetCityList", cvm);
        }

        public IActionResult CreateVenue()
        {
            var cities = new SelectList(cityService.GetCities(), "Id", "Name");
            ViewBag.Cities = cities;
            return View("Venue/CreateVenue");
        }

        [HttpPost]
        public async Task<IActionResult> CreateVenue(VenueCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var venue = new Venue
                {
                    Name = model.Name,
                    Adress = model.Adress,
                    CityId = model.CityId,
                };

                await venueService.Save(venue);
                return RedirectToAction("Venue/GetVenueList");
            }

            return RedirectToAction("Venue/CreateVenue");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditVenue(int id)
        {
            var cities = new SelectList(cityService.GetCities(), "Id", "Name");
            ViewBag.Cities = cities;
            var venue = venueService.GetVenueById(id);
            return View("Venue/EditVenue",venue);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> EditVenue(Venue model)
        {
            if (ModelState.IsValid)
            {
                await venueService.EditSave(model);
                return RedirectToAction("Venue/GetVenueList");
            }
            return NotFound();
        }

        [ActionName("DeleteVenue")]
        public IActionResult ConfirmDeleteVenue(int? id)
        {
            if (id != null)
            {
                Venue venue = venueService.GetVenueById(id);
                return PartialView("Venue/_DeleteVenue", venue);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVenue(int? id)
        {
            if (id != null)
            {
                var venue = venueService.GetVenueById(id);
                if (venue != null)
                {
                    await venueService.Delete(venue);
                    return RedirectToAction("Venue/GetVenueList");
                }
            }
            return NotFound();
        }

        public IActionResult GetVenueList()
        {
            var vvm = new VenueViewModel
            {
                Venues = venueService.GetVenues().ToArray()
            };
            return View("Venue/GetVenueList" ,vvm);
        }
    }
}
