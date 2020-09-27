using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business;

namespace Task1_Homework.Business
{
    public class UserList
    {
        public List<User> users = new List<User>();
   
        public UserList()
        {
            users.AddRange(
                new[]
                {
                    new User { Id = 1, FirstName = "User1", Password = "1111", Role = "User", Avatar = "a1.jpg" },
                    new User { Id = 2, FirstName = "User2", Password = "2222", Role = "User", Avatar = "a2.jpg" },
                    new User { Id = 3, FirstName = "User3", Password = "3333", Role = "User", Avatar = "a3.jpg" },
                    new User { Id = 4, FirstName = "Admin", Password = "admin", Role = "Administrator", Avatar = "a.jpg" },
                });
        }

        public bool ValidatePassword(string userName, string password)
        {
            var user = users.FirstOrDefault(u => u.FirstName.Equals(userName));
            if (user != null)
            {
                return user.Password.Equals(password);
            }

            throw new ArgumentException("User not found");

        }
        public User[] GetUser()
        {
            return users.ToArray();
        }
        public User GetUserById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public string GetRole(string userName) => users.FirstOrDefault(u => u.FirstName.Equals(userName))?.Role;
    }
}
