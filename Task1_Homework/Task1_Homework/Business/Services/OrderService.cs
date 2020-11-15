﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Services;

namespace Task1_Homework.Business.Models
{
    public class OrderService : ICRUD<Order>
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

        public async Task<Order> GetOrderById(int? id)
        {
            var order = await context.Orders
                .Include(o => o.Buyer)
                .Include(o => o.Ticket)
                .ThenInclude(ot => ot.Seller)
                .Include(o => o.Ticket)
                .ThenInclude(ot => ot.Event)
                .ThenInclude(ote => ote.Venue)
                .ThenInclude(otev => otev.City)
                .SingleOrDefaultAsync(o => o.Id == id);
            return order;
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

        public async Task Delete(Order model)
        {
            context.Orders.Remove(model);
            await context.SaveChangesAsync();
        }
    }
}
