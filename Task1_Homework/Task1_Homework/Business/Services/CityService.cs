using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Services;
using Task1_Homework.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;
using Task1_Homework.Business.Services.IServices;

namespace Task1_Homework.Business
{
    public class CityService : ICRUD<City>, ICityService

    {
        private readonly ResaleContext context;

        public CityService(ResaleContext context)
        {
            this.context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return context.Cities.ToArray();
        }

        public async Task<City> GetCityById(int? id)
        {
            return await context.Cities.FindAsync(id);
        }

        public async Task Save(City model)
        {
            await context.Cities.AddAsync(model);
            await context.SaveChangesAsync();
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
