using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business;

namespace Task1_Homework.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Avatar { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
