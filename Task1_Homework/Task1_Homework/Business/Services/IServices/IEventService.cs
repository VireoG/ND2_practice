using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Queries;

namespace Task1_Homework.Business.Services.IServices
{
    public interface IEventService : ICRUD<Event>
    {
        void GetEntry(Event @event);
        bool EventExists(int id);
        Event GetEventById(int? id);
        Task<IEnumerable<Event>> GetEvents();
        Task<IEnumerable<Event>> GetFiltredEvents(PagedData<Event> pagedData);
        Task<IEnumerable<Event>> GetEventByCity(int cityId);
        IQueryable<Event> GetEventByVenue(int venueId);
        Task<IEnumerable<Event>> GetEventsWithOutDependencies();
    }
}
