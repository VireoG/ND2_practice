using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;

namespace Task1_Homework.Business.Models
{
    public class OrderService
    {
        private readonly ResaleContext context;
        private readonly List<Order> orders = new List<Order>();

        public OrderService(ResaleContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var order = context.Orders
                .Include(o => o.Ticket)
                .Include(o => o.Buyer);
            return await order.ToArrayAsync();
        }

        public Order[] AddOrder(Order order)
        {
            context.Orders.Add(order);
            return context.Orders.ToArray();
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await GetOrder(id);
            return order;
        }

        private async Task<Order> GetOrder(int id)
        {
            var order = await context.Orders
                .Include(o => o.Ticket)
                .Include(o => o.Buyer)
                .SingleOrDefaultAsync(o => o.Id == id);
            return order;
        }
    }
}
