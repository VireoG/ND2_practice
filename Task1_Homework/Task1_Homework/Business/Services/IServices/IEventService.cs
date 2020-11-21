using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Services.IServices
{
    public interface IEventService : ICRUD<Event>
    {
        Task<Event> GetEventById(int? id);
        Task<IEnumerable<Event>> GetEvents();
    }
}
