using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            return venue.ToArray();
        }

        public async Task<Venue> GetVenueById(int? id)
        {
            var venue = await context.Venues.FindAsync(id);
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

