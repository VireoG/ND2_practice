using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Models;
using Task1_Homework.Business.Services.IServices;

namespace Task1_Homework.Business
{
    public class UserService : IUserService
    {
        private readonly ResaleContext context;
        private readonly TicketService ticketService;
        private readonly UserManager<User> userManager;

        public UserService(ResaleContext context, UserManager<User> userManager)
        {
            ticketService = new TicketService(context);
            this.context = context;
            this.userManager = userManager;
        }

        public bool ValidatePassword(string userName, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName.Equals(userName));
            if (user != null)
            {
                return user.PasswordHash.Equals(password);
            }

            throw new ArgumentException("User not found");

        }
        public async Task<IEnumerable<User>> GetUsers() => await context.Users.ToArrayAsync();

        public async Task<User> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<User> GetUserByIdentityName(string Name)
        {
            var user = await userManager.FindByNameAsync(Name);
            return user;
        }

        public async Task<List<Ticket>> UserTickets(User model)
        {
            var selectedTickets = from ticket in await ticketService.GetTickets()
                                  where ticket.Seller.UserName == model.UserName
                                  select ticket;

            return selectedTickets.ToList();
        }

        public async Task Save(User model)
        {
            context.Users.Update(model);
            await context.SaveChangesAsync();
        }

        public async Task EditSave(User model)
        {
            context.Users.Update(model);
            await context.SaveChangesAsync();
        }

        public async Task Delete(User model)
        {
            context.Users.Remove(model);
            await context.SaveChangesAsync();
        }   
    }
}
