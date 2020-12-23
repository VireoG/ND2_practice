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
using Task1_Homework.Business.Services.IServices;

namespace Task1_Homework.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;

        public UserController(UserManager<User> userManager, IUserService userService)
        { 
            this.userManager = userManager;
            this.userService = userService;
        }

        public async Task<IActionResult> Profile()
        {
            var model = await userService.GetUserByIdentityName(User.Identity.Name);
            model.Tickets = await userService.UserTickets(model);

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

        public IActionResult ManageProfile()
        {
            return LocalRedirect("/Identity/Account/Manage");
        }

        public async Task<IActionResult> ChangeAvatar([FromRoute] string id)
        {
            if (id != null)
            {
                var user = await userService.GetUserById(id);

                var model = new ChangeAvatarViewModel
                {
                    UserName = user.UserName,
                    Avatar = user.Avatar
                };
                return PartialView("_ChangeAvatar", model);
            }
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAvatar(ChangeAvatarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userService.GetUserByIdentityName(User.Identity.Name);
                user.Avatar = model.Avatar;

                await userService.EditSave(user);
                return RedirectToAction("Profile");
            }
            return RedirectToAction("ChangeAvatar", model);
        }
    }
}
