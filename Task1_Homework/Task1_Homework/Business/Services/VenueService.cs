using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Services;
using Task1_Homework.Business.Services.IServices;

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
            var venue = from item in GetVenues()
                        where item.City.Id == id
                        select item;
            return venue;
        }

        public async Task Save(Venue model)
        {
            await context.Venues.AddAsync(model);
            await context.SaveChangesAsync();
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

