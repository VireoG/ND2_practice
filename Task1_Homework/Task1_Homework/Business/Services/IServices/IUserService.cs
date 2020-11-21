using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business.Services.IServices
{
    public interface IUserService : ICRUD<User>
    {
        bool ValidatePassword(string userName, string password);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(string id);
        Task<User> GetUserByIdentityName(string Name);
        Task<List<Ticket>> UserTickets(User model);
    }
}
