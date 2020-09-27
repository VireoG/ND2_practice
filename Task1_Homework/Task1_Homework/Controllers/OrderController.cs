using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task1_Homework.Business;
using Task1_Homework.Business.Models;
using Task1_Homework.Models;

namespace Task1_Homework.Controllers
{
    public class OrderController : Controller
    {
        private TicketList ticketList;
        private EventList eventList;
        public OrderList orderList = new OrderList();
        public Order order = new Order();

        public OrderController()
        {
            ticketList = new TicketList();
            eventList = new EventList();
        }

        public IActionResult Index([FromRoute] int id, string UserName)
        {
            var selectedTicket = from ticket in ticketList.tickets
                           where ticket.Id == id
                           select ticket;

            order.Ticket = selectedTicket.ElementAt(0);
            order.Buyer = UserName;

            var selectedEvent = from eventt in eventList.events
                           where eventt.Name == order.Ticket.EventName
                           select eventt;

            order.Event = selectedEvent.ElementAt(0);
            orderList.orders.Add(order);
            return View(order);
        }

        public IActionResult BuyRequest()
        {
            
            return View();
        }

        public IActionResult SalesRequests()
        {
             var selected = from orders in orderList.orders
                            where orders.Ticket.Seller == User.Identity.Name
                           select orders;

            var model = new OrderViewModel
            {
                Orders = orderList.GetOrder()
            };
            
            return View(model);
        }
    }
}
