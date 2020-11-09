using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business;
using Task1_Homework.Business.Database;

namespace Task1_Homework.Business
{
    public class UserService
    {
        private readonly ResaleContext context;
   
        public UserService(ResaleContext context)
        {
            this.context = context;
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

        public User GetUserById(string id)
        {
            var user = context.Users.SingleOrDefault(o => o.Id == id);
            return user;
        }

        public async Task SaveChanges(User model)
        {
            context.Users.Update(model);
            await context.SaveChangesAsync();
        }
    }
}
