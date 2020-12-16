using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Services;
using Task1_Homework.Business.Services.IServices;
using Task1_Homework.Business.Queries;

namespace Task1_Homework.Business
{
    public class VenueService : IVenueService
    {
        private readonly ResaleContext context;
        public VenueService(ResaleContext context)
        {
            this.context = context;
        }

        public IEnumerable<Venue> GetVenues()
        {
            var venue = context.Venues
                   .Include(v => v.City);
            return venue.ToArrayAsync().Result;
        }

        public Venue GetVenueById(int? id)
        {
            var venue = GetVenues().SingleOrDefault(v => v.Id == id);
            return venue;
        }

        public IEnumerable<Venue> GetVenuesByCity(int? id)
        {
            var venue = from item in context.Venues.ToArray()
                        where item.CityId == id
                        select item;
            return venue;
        }

        public async Task<PagedResult<Venue>> GetVenuesByCities(VenueQuery query)
        {
            var queryable = context.Venues.AsQueryable();

            if (query.cities != null)
            {
                queryable = queryable.Where(c => query.cities.Contains(c.CityId));
            }

            var items = await queryable.ToListAsync();

            return new PagedResult<Venue> { Items = items };
        }

        public async Task Save(Venue model)
        {
            var a = 0;
            foreach (var item in context.Venues)
            {
                if (item == model)
                {
                    a++;
                }
            }

            if (a == 0)
            {
                await context.Venues.AddAsync(model);
                await context.SaveChangesAsync();
            }
        }

        public async Task EditSave(Venue model)
        {

            context.Venues.Update(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Venue model)
        {
            context.Venues.Remove(model);
            await context.SaveChangesAsync();
        }
    }
}

