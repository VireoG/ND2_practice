using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task1_Homework.Business;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Models;
using Task1_Homework.Business.Services.IServices;
using Task1_Homework.Models;

namespace Task1_Homework.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITicketService ticketService;
        private readonly IEventService eventService;

        public TicketController(UserManager<User> userManager, ITicketService ticketService, IEventService eventService)
        {
            this.userManager = userManager;
            this.ticketService = ticketService;
            this.eventService = eventService;
        }

        public IActionResult CreateTicket([FromRoute] int? id)
        {
            if (id != null)
            {
                var @event = eventService.GetEventById(id);

                if (@event == null)
                {
                    return BadRequest();
                }

                var model = new TicketCreateViewModel
                {
                    EventId = @event.Id,
                    EventName = @event.Name
                };
                return PartialView("CreateTicket", model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ticket = new Ticket
                {
                    EventId = model.EventId,
                    Price = model.Price,
                    SellerId = (await userManager.FindByNameAsync(User.Identity.Name)).Id
                };

                ticket.Status = TicketSaleStatus.Sale;
                await ticketService.Save(ticket);
                return RedirectToAction("Index", "Event");
            }
            
            return RedirectToAction("CreateTicket", model);
        }

        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var ticket = ticketService.GetTicketById(id);
                return View("Edit", ticket);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Ticket model)
        {
            if (ModelState.IsValid)
            {
                await ticketService.EditSave(model);
                return RedirectToAction("Index");
            }

            return BadRequest();
        }


        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var ticket = ticketService.GetTicketById(id);
                return PartialView("_Delete", ticket);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var ticket = ticketService.GetTicketById(id);
                if (ticket != null)
                {
                    await ticketService.Delete(ticket);
                    return RedirectToAction("Profile", "User");
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> UserTickets()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var tickets = await ticketService.GetTicketsByUserId(user.Id);
            return View("UserTickets", tickets);
        }
    }
}
