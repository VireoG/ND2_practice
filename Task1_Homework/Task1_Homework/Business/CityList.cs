using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business
{
    public class CityList
    {
        public List<City> cities = new List<City>();
        public CityList()
        {
            cities.AddRange(
            new[]
                {
                    new City { Id = 1, Name = "Minsk"},
                    new City { Id = 2, Name = "Moscow"},
                    new City { Id = 3, Name = "London"},
                    new City { Id = 4, Name = "New-York"},
                    new City { Id = 5, Name = "Tokyo"},
                    new City { Id = 6, Name = "Homel"}
                });
        }

        public City[] GetCity()
        {
            return cities.ToArray();
        }

        public City GetCityById(int id)
        {
            return cities.FirstOrDefault(c => c.Id == id);
        }
    }
}
