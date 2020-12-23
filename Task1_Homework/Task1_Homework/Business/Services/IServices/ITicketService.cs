using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Services.IServices
{
    public interface ITicketService : ICRUD<Ticket>
    {
        Task<IEnumerable<Ticket>> GetTickets();
        Ticket GetTicketById(int? id);
        Task<IEnumerable<Ticket>> GetTicketsByUserId(string id);
        Task<List<Ticket>> GetTicketsByEventIdForIdentityUser(int? id, string UserName);
    }
}
