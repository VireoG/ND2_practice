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
using Task1_Homework.Models;

namespace Task1_Homework.Controllers
{
    public class TicketController : Controller
    {
        private readonly ResaleContext context;
        private readonly UserManager<User> userManager;
        private readonly TicketService ticketService;
        private readonly EventService eventService;
        private readonly UserService userService;

        public TicketController(ResaleContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            ticketService = new TicketService(context);
            eventService = new EventService(context);
            userService = new UserService(context, userManager);
        }

        private async Task<List<Ticket>> GetTicketsListForIdentityUser()
        {
            var selected = from item in await ticketService.GetTickets()
                           where item.Seller.UserName != User.Identity.Name
                           select item;

            return selected.ToList();
        }

        [Authorize]
        public async Task<IActionResult> CreateTicket([FromRoute] int id)
        {
            var @event = await context.Events.FindAsync(id);
            if (@event == null)
            {
                return BadRequest();
            }
            
            var model = new TicketCreateViewModel
            {
                EventId = @event.Id,
                EventName = @event.Name
            };
            return View("CreateTicket", model);
        }

        [Authorize]
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
                await ticketService.Save(ticket);
                return RedirectToAction("Index", "Event");
            }
            
            return RedirectToAction("CreateTicket", model);
        }


        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
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


        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Ticket ticket = await ticketService.GetTicketById(id);
                return PartialView("_Delete", ticket);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var ticket = await ticketService.GetTicketById(id);
                if (ticket != null)
                {
                    await ticketService.Delete(ticket);
                    return RedirectToAction("Profile", "User");
                }
            }
            return NotFound();
        }
    }
}
