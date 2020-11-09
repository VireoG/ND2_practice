using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;

namespace Task1_Homework.Business
{
    public class CityService
    {
        private readonly ResaleContext context;

        public CityService(ResaleContext context)
        {
            this.context = context;
        }

        public City[] GetCities()
        {
            return context.Cities.ToArray();
        }

        public async Task<City> GetCityById(int id)
        {
            return await context.Cities.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task Save(City model)
        {
            await context.Cities.AddAsync(model);
            await context.SaveChangesAsync();
        }
    }
}
