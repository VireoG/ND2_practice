using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1_Homework.Business;
using Task1_Homework.Models;
using System.Security.Principal;
using Task1_Homework.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Task1_Homework.Business.Database;
using System.IO;

namespace Task1_Homework.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;
        private readonly ResaleContext context;
        private readonly TicketService ticketService;


        public UserController(ResaleContext context)
        {
            userService = new UserService(context);
            this.context = context;
            ticketService = new TicketService(context);
        }

        [Authorize]
        public IActionResult Profile()
        {                 
            var selectedUsers = from user in context.Users
                           where user.UserName == User.Identity.Name
                           select user;

            User model = selectedUsers.SingleOrDefault();

            model.Tickets = UserTickets(model);

            return View("Profile", model);
        }

        public IActionResult EditProfile()
        {
            return View("EditProfile");
        }

        private List<Ticket> UserTickets(User model)
        {
            var selectedTickets = from ticket in ticketService.GetTickets().Result.ToArray()
                                  where ticket.SellerName == model.UserName
                                  select ticket;

            return selectedTickets.ToList();
        }

        public async Task<IActionResult> ChangeAvatar([FromRoute] string id)
        {
            var user = userService.GetUserById(id);

            byte[] imageData = null;
           
            using (var binaryReader = new BinaryReader(pvm.Avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)pvm.Avatar.Length);
            }
     
            user.Avatar = imageData;

            await userService.SaveChanges(user);
            return RedirectToAction("Profile");
        }
    }
}
