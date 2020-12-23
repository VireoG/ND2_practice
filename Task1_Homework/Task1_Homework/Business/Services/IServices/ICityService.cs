using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Services.IServices
{
    public interface ICityService : ICRUD<City>
    {
        IEnumerable<City> GetCities();

        Task<City> GetCityById(int? id);
    }
}
