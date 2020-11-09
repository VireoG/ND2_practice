using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;

namespace Task1_Homework.Business
{
    public class VenueService
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

        public async Task<Venue> GetVenueById(int id)
        {
            var venue = await GetVenue(id);
            return venue;
        }

        private async Task<Venue> GetVenue(int id)
        {
            var venue = await context.Venues
                .Include(v => v.City)              
                .SingleOrDefaultAsync(o => o.Id == id);
            return venue;
        }

        public async Task Save(Venue model)
        {
            await context.Venues.AddAsync(model);
            await context.SaveChangesAsync();
        }
    }
}

