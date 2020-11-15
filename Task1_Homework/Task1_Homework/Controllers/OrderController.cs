using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task1_Homework.Business;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Models;
using Task1_Homework.Models;

namespace Task1_Homework.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ResaleContext context;
        private readonly TicketService ticketService;
        private readonly OrderService orderService;

        public OrderController(ResaleContext context)
        {
            orderService = new OrderService(context);
            ticketService = new TicketService(context);
            this.context = context;
        }

        public IActionResult Create([FromRoute] int id)
        {

            var model = new OrderCreateViewModel
            {
                TicketId = ticketService.GetTicketById(id).Result.Id,
                TicketPrice = ticketService.GetTicketById(id).Result.Price,
                BuyerName = context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name).UserName,
                BuyerId = context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name).Id,
                EventName = ticketService.GetTicketById(id).Result.Event.Name
            };
            return View("Create", model);
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
            var selected = from orders in orderService.GetOrders().Result
                           where orders.Ticket.Seller.UserName == User.Identity.Name
                           select orders;

            var model = new OrderViewModel
            {
                Orders = selected.ToArray()
            };
            
            return View(model);
        }


        public async Task<IActionResult> Accept([FromRoute]int? id)
        {
            if (id != null)
            {
                var order = await orderService.GetOrderById(id);

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
                    model.Status = TicketSaleStatus.Sold;
                    await orderService.EditSave(model);
                    return RedirectToAction("SalesRequests");
            }
            return NotFound();
        }

        public IActionResult MyOrders()
        {
            var selected = from orders in orderService.GetOrders().Result
                           where orders.Buyer.UserName == User.Identity.Name
                           select orders;

            var model = new OrderViewModel
            {
                Orders = selected.ToArray()
            };

            return View(model);
        }

        public async Task<IActionResult> Reject([FromRoute]int? id)
        {
            if (id != null)
            {
                var order = await orderService.GetOrderById(id);
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
                await orderService.Delete(await orderService.GetOrderById(id));
                return RedirectToAction("MyOrders");
            }

            return NotFound();
        }
    }
}
