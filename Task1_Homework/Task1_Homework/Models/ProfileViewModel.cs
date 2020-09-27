using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business;

namespace Task1_Homework.Models
{
    public class ProfileViewModel
    {
        public User[] Users { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Avatar { get; set; }

        public string Role { get; set; }

        public string Localization { get; set; }

        public string Adress { get; set; }

        public string PhoneNumber { get; set; }
    }
}
