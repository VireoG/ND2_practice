using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Services.IServices
{
    public interface IVenueService : ICRUD<Venue>
    {
        IEnumerable<Venue> GetVenues();
        Task<Venue> GetVenueById(int? id);
    }
}
