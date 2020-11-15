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
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using Task1_Homework.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Task1_Homework.Business.Database;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace Task1_Homework.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;
        private readonly ResaleContext context;
        private readonly UserManager<User> userManager;
        private readonly TicketService ticketService;


        public UserController(ResaleContext context, UserManager<User> userManager)
        {
            userService = new UserService(context, userManager);
            this.context = context;
            this.userManager = userManager;
            ticketService = new TicketService(context);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var model = await userService.GetUserByIdentityName(User.Identity.Name);
            model.Tickets = userService.UserTickets(model);

            if (model != null)
            {
                var uvm = new UserViewModel
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    Avatar = model.Avatar,
                    Tickets = model.Tickets
                };
                return View("Profile", uvm);
            }

            return NotFound();
        }

        public async Task<IActionResult> ChangeAvatar([FromRoute] string id)
        {
            var user = userService.GetUserById(id).Result;

            byte[] imageData = null;
          
            user.Avatar = imageData;

            await userService.SaveChanges(user);
            return RedirectToAction("Profile");
        }
    }
}
