using System;
using Task1_Homework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Task1_Homework.Business.Database;
using Microsoft.EntityFrameworkCore;

namespace Task1_Homework.Business
{
    public class EventService 
    {
        private readonly ResaleContext context;

        public EventService(ResaleContext context)
        {
            this.context = context;
        }

        public async Task<Event> GetEventById(int? id)
        {
            var ev = await context.Events.FindAsync(id);
            return ev;
        }

        private async Task<Event> GetEvent(int id)
        {
            var ev = await context.Events
                .Include(e => e.Venue)
                .ThenInclude(eс => eс.City)
                .SingleOrDefaultAsync(e => e.Id == id);
            return ev;
        }

        public async Task Save(Event model)
        {
            await context.Events.AddAsync(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var ev = await context.Events.FindAsync(id);
            context.Events.Remove(ev);
            await context.SaveChangesAsync();
        }

        public async Task EditSave(Event model)
        {
            context.Events.Update(model);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            var ev =  context.Events
                .Include(e => e.Venue)
                .ThenInclude(eс => eс.City);               
            return await ev.ToArrayAsync();
        }
    }
}
