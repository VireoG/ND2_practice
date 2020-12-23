using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Services;
using Task1_Homework.Business.Services.IServices;

namespace Task1_Homework.Business.Models
{
    public class OrderService : IOrderService
    {
        private readonly ResaleContext context;

        public OrderService(ResaleContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var order = context.Orders
                .Include(o => o.Buyer)
                .Include(o => o.Ticket)
                .ThenInclude(ot => ot.Seller)
                .Include(o => o.Ticket)
                .ThenInclude(ot => ot.Event)
                .ThenInclude(ote => ote.Venue)
                .ThenInclude(otev => otev.City);
            return await order.ToArrayAsync();
        }

        public Order GetOrderById(int? id)
        {
            var order = GetOrders().Result.SingleOrDefault(o => o.Id == id);
            return order;
        }

        public async Task<IEnumerable<Order>> GetSalesRequestsForIdentityUser(string UserName)
        {
            var selected = from orders in await GetOrders()
                           where orders.Ticket.Seller.UserName == UserName
                           select orders;

            return selected;
        }

        public async Task<IEnumerable<Order>> GetOrdersForIdentityUser(string UserName)
        {
            var selected = from orders in await GetOrders()
                           where orders.Buyer.UserName == UserName
                           select orders;

            return selected;
        }

        public async Task Save(Order model)
        {
            await context.Orders.AddAsync(model);
            await context.SaveChangesAsync();
        }

        public async Task EditSave(Order model)
        {
            context.Orders.Update(model);
            await context.SaveChangesAsync();
        }

        public async Task EditSave(Order model, string TrackNumber)
        {
            model.Status = TicketSaleStatus.Sold;
            model.TrackNumber = TrackNumber;
            model.Ticket.Status = TicketSaleStatus.Sold;
            context.Orders.Update(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Order model)
        {
            context.Orders.Remove(model);
            await context.SaveChangesAsync();
        }
    }
}
