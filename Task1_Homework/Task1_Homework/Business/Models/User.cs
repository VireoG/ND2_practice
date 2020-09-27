using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Homework.Business
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Avatar { get; set; }

        public string Role { get; set; }

        public string Localization { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }

        public List<Order> Orders { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
