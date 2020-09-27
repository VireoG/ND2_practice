using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business
{
    public class VenueList
    {
        public List<Venue> venues = new List<Venue>();
        public VenueList()
        {
            venues.AddRange(
            new[]
                {
                    new Venue { Id = 1, Name = "Prime Hall", Adress = "Somewhere in Minsk", CityId = 1},
                    new Venue { Id = 2, Name = "Area 1.2", Adress = "Somewhere in Minsk", CityId = 1},
                    new Venue { Id = 3, Name = "Area 2.1", Adress = "Somewhere in Moscow", CityId = 2},
                    new Venue { Id = 4, Name = "Area 2.2", Adress = "Somewhere in Moscow", CityId = 2},
                    new Venue { Id = 5, Name = "Area 3.1", Adress = "Somewhere in London", CityId = 3 },
                    new Venue { Id = 6, Name = "Area 3.2", Adress = "Somewhere in London", CityId = 3},
                    new Venue { Id = 7, Name = "Area 4.1", Adress = "Somewhere in New-York", CityId = 4},
                    new Venue { Id = 8, Name = "Area 4.2", Adress = "Somewhere in New-York", CityId = 4},
                    new Venue { Id = 9, Name = "Area 5.1", Adress = "Somewhere in Tokyo", CityId = 5},
                    new Venue { Id = 10, Name = "Area 5.2", Adress = "Somewhere in Tokyo", CityId = 5},
                    new Venue { Id = 11, Name = "Area 6.1", Adress = "Somewhere in Homel", CityId = 6},
                    new Venue { Id = 12, Name = "Area 6.2", Adress = "Somewhere in Homel", CityId = 6},

                });
        }

        public Venue[] GetCity()
        {
            return venues.ToArray();
        }

        public Venue GetCityById(int id)
        {
            return venues.FirstOrDefault(c => c.Id == id);
        }
    }
}

