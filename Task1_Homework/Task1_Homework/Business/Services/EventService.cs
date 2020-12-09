using System;
using Task1_Homework.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Task1_Homework.Business.Database;
using Microsoft.EntityFrameworkCore;
using Task1_Homework.Business.Services.IServices;
using Task1_Homework.Business.Queries;

namespace Task1_Homework.Business
{
    public class EventService : IEventService
    {
        private readonly ResaleContext context;
        private readonly IVenueService venueService;
        private readonly ISortingProvider<Event> sortingProvider;

        public EventService(ResaleContext context, ISortingProvider<Event> sortingProvider, IVenueService venueService)
        {
            this.context = context;
            this.sortingProvider = sortingProvider;
            this.venueService = venueService;
        }

        public Event GetEventById(int? id)
        {
            var ev = GetEvents().Result.SingleOrDefault(e => e.Id == id);
            return ev;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            var ev = context.Events
                .Include(e => e.Venue)
                .ThenInclude(eс => eс.City);
            return await ev.ToArrayAsync();
        }

        public async Task<IEnumerable<Event>> GetEventByCity(int id)
        {
            var ev = from events in await GetEvents()
                     where events.Venue.City.Id == id
                     select events;
            return ev;
        }

        public async Task<PagedResult<Event>> GetEvents(EventQuery query)
        {
            var queryable = context.Events.AsQueryable();

            if (query.Cities != null)
            {
                queryable = queryable.Where(c => query.Cities.Contains(c.Venue.City.Id) && query.Venues.Contains(c.Venue.Id));               
            }

            var count = await queryable.CountAsync();

            queryable = sortingProvider.ApplySorting(queryable, query);
            queryable = queryable.ApplyPagination(query);

            var items = await queryable.ToListAsync();

            return new PagedResult<Event> { TotalCount = count, Items = items };
        }

        public IQueryable<Event> GetEventByVenue(int id)
        {
            var ev = GetEvents().Result.Where(e => e.Venue.Id == id);
            return ev.AsQueryable();
        }

        public bool EventExists(int id)
        {
            return context.Events.Any(e => e.Id == id);
        }

        public void GetEntry(Event @event)
        {
            context.Entry(@event).State = EntityState.Modified;
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
