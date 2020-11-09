﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1_Homework.Business.Database;

namespace Task1_Homework.Business
{
    public class User : IdentityUser
    {
        public byte[] Avatar { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
