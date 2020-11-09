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
    public class OrderController : Controller
    {
        private readonly ResaleContext context;
        private readonly Order order;
        private readonly TicketService ticketService;
        private readonly OrderService orderService;



        public OrderController(ResaleContext context)
        {
            orderService = new OrderService(context);
            ticketService = new TicketService(context);
            this.context = context;
            order = new Order();
        }

        [Authorize]
        public IActionResult Index([FromRoute] int id)
        {        
            order.Ticket = ticketService.GetTicketById(id).Result;

            foreach (var user in context.Users)
            {
                if (user.UserName == User.Identity.Name)
                {
                    order.Buyer = user;
                }
            }

            orderService.AddOrder(order);

            return View(order);
        }

        [Authorize]
        public IActionResult BuyRequest()
        {            
            return View();
        }

        [Authorize]
        public IActionResult SalesRequests()
        {
            var selected = from orders in context.Orders
                           where orders.Ticket.Seller.UserName == User.Identity.Name
                           select orders;

            var model = new OrderViewModel
            {
                Orders = context.Orders.ToArray()
            };
            
            return View(model);
        }
    }
}
