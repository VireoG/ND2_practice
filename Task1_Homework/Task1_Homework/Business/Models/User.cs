using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;
using Task1_Homework.Business.Models;

namespace Task1_Homework.Business
{
    public class User : IdentityUser
    {
        public string Avatar { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
