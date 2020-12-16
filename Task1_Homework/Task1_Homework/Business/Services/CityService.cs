using System.Collections.Generic;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;
using Task1_Homework.Business.Services.IServices;
using System.Linq;

namespace Task1_Homework.Business
{
    public class CityService : ICityService

    {
        private readonly ResaleContext context;

        public CityService(ResaleContext context)
        {
            this.context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return context.Cities.ToArrayAsync().Result;
        }

        public async Task<City> GetCityById(int? id)
        {
            return await context.Cities.FindAsync(id);
        }

        public async Task Save(City model)
        {
            var a = 0;
            foreach(var item in context.Cities)
            {
                if(item == model)
                {
                    a++;
                }
            }

            if (a == 0)
            {
                await context.Cities.AddAsync(model);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(City model)
        {
            context.Cities.Update(model);
            await context.SaveChangesAsync();
        }

        public async Task EditSave(City model)
        {
            context.Cities.Remove(model);
            await context.SaveChangesAsync();
        }
    }
}
