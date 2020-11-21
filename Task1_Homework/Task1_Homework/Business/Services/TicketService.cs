﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Services;

namespace Task1_Homework.Business.Models
{
    public class TicketService : ICRUD<Ticket>
    {
        private readonly ResaleContext context;

        public TicketService(ResaleContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            var ticket = context.Tickets
               .Include(t => t.Seller)
               .Include(t => t.Event)
               .ThenInclude(te => te.Venue)
               .ThenInclude(tec => tec.City);

            return await ticket.ToArrayAsync();
        }

        public async Task<Ticket> GetTicketById(int? id)
        {
            var ticket = await GetTicket(id);
            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserId(string id)
        {
            var tickets = from item in await GetTickets()
                           where item.SellerId == id 
                           select item;

            return tickets.ToArray();
        }

        private async Task<Ticket> GetTicket(int? id)
        {
            var ticket = await context.Tickets
                .Include(t => t.Seller)
                .Include(t => t.Event)
                .ThenInclude(te => te.Venue)
                .ThenInclude(tec => tec.City)
                .SingleOrDefaultAsync(t => t.Id == id);
            return ticket;
        }


        public async Task Save(Ticket model)
        {
            await context.Tickets.AddAsync(model);
            await context.SaveChangesAsync();
        }

        public async Task EditSave(Ticket model)
        {
            context.Tickets.Update(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Ticket model)
        {
            context.Tickets.Remove(model);
            await context.SaveChangesAsync();
        }
    }
}
