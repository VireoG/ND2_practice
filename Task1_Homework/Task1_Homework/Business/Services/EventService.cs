using System;
using Task1_Homework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Task1_Homework.Business.Database;
using Microsoft.EntityFrameworkCore;
using Task1_Homework.Business.Services;

namespace Task1_Homework.Business
{
    public class EventService : ICRUD<Event>
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

        public async Task<IEnumerable<Event>> GetEvents()
        {
            var ev = context.Events
                .Include(e => e.Venue)
                .ThenInclude(eс => eс.City);
            return await ev.ToArrayAsync();
        }

        public async Task Save(Event model)
        {
            await context.Events.AddAsync(model);
            await context.SaveChangesAsync();
        }

        public async Task EditSave(Event model)
        {
            context.Events.Update(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Event model)
        {
            context.Events.Remove(model);
            await context.SaveChangesAsync();
        }
    }
}
