using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task1_Homework.Business;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Models;
using Task1_Homework.Business.Services.IServices;
using Task1_Homework.Models;

namespace Task1_Homework.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IOrderService orderService;
        private readonly ITicketService ticketService;

        public OrderController(UserManager<User> userManager, IOrderService orderService, ITicketService ticketService)
        {
            this.userManager = userManager;
            this.orderService = orderService;
            this.ticketService = ticketService;
        }

        public async Task<IActionResult> Create([FromRoute] int? id)
        {
            if (id != null)
            {
                var ticket = ticketService.GetTicketById(id);
                var buyer = await userManager.FindByNameAsync(User.Identity.Name);

                var model = new OrderCreateViewModel
                {
                    TicketId = ticket.Id,
                    BuyerId = buyer.Id
                };

                ViewData["EventName"] = ticket.Event.Name;
                ViewData["TicketPrice"] = ticket.Price;
                ViewData["BuyerName"] = buyer.UserName;

                return View("Create", model);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateViewModel model)
        {
            if (ModelState.IsValid) {
                var order = new Order
                {
                    TicketId = model.TicketId,
                    BuyerId = model.BuyerId,                       
                    Status = TicketSaleStatus.Confirmation
                };
                await orderService.Save(order);
                return View("BuyRequest");
            }
            return RedirectToAction("Create", model);
        }

        public IActionResult BuyRequest()
        {            
            return View();
        }

        public IActionResult SalesRequests()
        {
            var orders = orderService.GetSalesRequestsForIdentityUser(User.Identity.Name).Result;

            if (orders != null)
            {
                var model = new OrderViewModel
                {
                    Orders = orders.ToArray()
                };

                return View(model);
            }

            return NotFound();
        }


        public IActionResult Accept([FromRoute] int? id)
        {
            if (id != null)
            {
                var order = orderService.GetOrderById(id);

                if (order != null)
                {
                    return PartialView("_Accept", order);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Accept(Order model)
        {
            if (ModelState.IsValid)
            {
                var order = orderService.GetOrderById(model.Id);
                var user = await userManager.FindByNameAsync(HttpContext.User.Identity.Name);

                if (order.Ticket.SellerId != user.Id) 
                    return Forbid();

                await orderService.EditSave(order, model.TrackNumber);
                return RedirectToAction("SalesRequests");
            }
            return NotFound();
        }

        public IActionResult MyOrders()
        {
            var orders = orderService.GetOrdersForIdentityUser(User.Identity.Name).Result;

            if (orders != null)
            {
                var model = new OrderViewModel
                {
                    Orders = orders.ToArray()
                };

                return View(model);
            }

            return NotFound();
        }

        public async Task<IActionResult> Reject([FromRoute]int? id)
        {
            if (id != null)
            {
                var order = orderService.GetOrderById(id);
                order.Status = TicketSaleStatus.Rejected;
                await orderService.EditSave(order);

                return RedirectToAction("SalesRequests");
            }

            return NotFound();
        }

        public async Task<IActionResult> CancelOrder([FromRoute]int? id)
        {
            if (id != null)
            {
                await orderService.Delete(orderService.GetOrderById(id));
                return RedirectToAction("MyOrders");
            }

            return NotFound();
        }
    }
}
