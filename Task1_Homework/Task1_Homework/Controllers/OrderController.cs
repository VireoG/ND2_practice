using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task1_Homework.Business;
using Task1_Homework.Business.Models;
using Task1_Homework.Models;

namespace Task1_Homework.Controllers
{
    public class OrderController : Controller
    {
        private readonly TicketList ticketList;
        private readonly EventList eventList;
        private readonly OrderList orderList;
        private readonly Order order;
        private readonly UserList userList;

        public OrderController(OrderList orderList,TicketList ticketList, EventList eventList, UserList userList)
        {
            this.orderList = orderList;
            order = new Order();
            this.ticketList = ticketList;
            this.eventList = eventList;
            this.userList = userList;
        }

        [Authorize]
        public IActionResult Index([FromRoute] int id)
        {        
            order.Ticket = ticketList.GetTicketById(id);

            foreach (var user in userList.GetUser())
            {
                if (user.FirstName == User.Identity.Name)
                {
                    order.Buyer = user;
                }
            }

            orderList.AddOrder(order);

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
            var selected = from orders in orderList.GetOrder()
                           where orders.Ticket.Seller.FirstName == User.Identity.Name
                           select orders;

            var model = new OrderViewModel
            {
                Orders = orderList.GetOrder()
            };
            
            return View(model);
        }
    }
}
