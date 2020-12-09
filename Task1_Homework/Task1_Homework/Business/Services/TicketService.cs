using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Services;
using Task1_Homework.Business.Services.IServices;

namespace Task1_Homework.Business.Models
{
    public class TicketService : ITicketService
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

        public Ticket GetTicketById(int? id)
        {
            var ticket = GetTickets().Result.SingleOrDefault(t => t.Id == id);
            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserId(string id)
        {
            var selected = from ticket in await GetTickets()
                           where ticket.SellerId == id 
                           select ticket;

            return selected.ToArray();
        }

        public async Task<List<Ticket>> GetTicketsByEventIdForIdentityUser(int? id, string UserName)
        {
            var selected = from ticket in await GetTickets()
                           where ticket.EventId == id && ticket.Seller.UserName != UserName
                           select ticket;

            return selected.ToList();
        }

        public IQueryable<Ticket> GetTicketsFilteredByPrice(decimal price)
        {
            var tickets = context.Tickets.Where(t => t.Price > price);

            return tickets;
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
