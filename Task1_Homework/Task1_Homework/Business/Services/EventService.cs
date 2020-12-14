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


        public async Task<IEnumerable<Event>> GetEventsWithOutDependencies()
        {
            var ev = await context.Events.ToListAsync();
            return ev;
        }

        public async Task<IEnumerable<Event>> GetEventByCity(int id)
        {
            var ev = from events in await GetEvents()
                     where events.Venue.City.Id == id
                     select events;
            return ev;
        }

        public IEnumerable<Event> GetEventsBySearch(string text)
        {
            var list = GetEventsNameForAutocomplete(text);
            var queryable = context.Events.AsQueryable();
            queryable = queryable.Where(c => list.Contains(c.Name));
            return queryable;
        }

        public IEnumerable<string> GetEventsNameForAutocomplete(string text)
        {
            var queryable = context.Events.AsQueryable();
            int equalChars = 0;
            var partiality = 1;
            var str1 = text.ToUpper();
            List<string> list = new List<string>();

            foreach (var ev in queryable)
            {
                var EvName = ev.Name.ToUpper();
                if (str1 == EvName)
                {
                    list.Add(ev.Name);
                }
                else if (EvName.StartsWith(str1) || EvName.EndsWith(str1))
                {
                    list.Add(ev.Name);
                }
                else { 
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (str1[i] == ' ')
                            str1.Remove(i);
                    }

                    for (int i = 0; i < EvName.Length; i++)
                    {
                        if (EvName[i] == ' ')
                            EvName.Remove(i);
                    }

                    string str2 =
                       !String.IsNullOrWhiteSpace(ev.Name) && ev.Name.Length >= text.Length
                       ? ev.Name.Substring(0, text.Length)
                       : ev.Name;                  

                    if (str2.Length == text.Length)
                    {
                        for (int i = 0; i < str1.Length; i++)
                        {
                            if (str1[i] == str2[i])
                                equalChars++;
                        }
                        if ((double)equalChars / str1.Length >= partiality)
                        {
                            list.Add(ev.Name);
                        }
                    }
                }
            }
            return list;
        }

        public IEnumerable<Event> GetFiltredEvents(PagedData<Event> pagedData)
        {
            var queryable = context.Events
                    .Include(e => e.Venue)
                    .ThenInclude(eс => eс.City)
                    .AsQueryable();

            if (pagedData.Search != null)
            {
                queryable = pagedData.Data.AsQueryable();
            }

            if (pagedData.Cities != null)
            {
                queryable = queryable.Where(c => pagedData.Cities.Contains(c.Venue.CityId));  
                if(pagedData.Venues != null)
                {
                    queryable = queryable.Where(c => pagedData.Venues.Contains(c.VenueId));
                }
            }           
            queryable = sortingProvider.ApplySorting(queryable, pagedData);
            var items = queryable.ToList();

            return items;
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
