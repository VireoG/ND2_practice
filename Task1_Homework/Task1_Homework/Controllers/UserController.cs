using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1_Homework.Business;
using Task1_Homework.Models;
using System.Security.Principal;
using Task1_Homework.Business.Models;

namespace Task1_Homework.Controllers
{
    public class UserController : Controller
    {
        private readonly UserList userList;
        public readonly TicketList ticketList;
        private string Pass { get; set; } 
        public UserController(UserList userList)
        {
            ticketList = new TicketList();
            this.userList = userList;
        }


        public IActionResult Login(string returnUrl)
        {
            var headers = Request.GetTypedHeaders();
            if (string.IsNullOrEmpty(returnUrl) && headers.Referer != null)
                returnUrl = HttpUtility.UrlEncode(headers.Referer.PathAndQuery);

            if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnURL = returnUrl;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginModel, string returnUrl)
        {       
            try
            {
                if (!userList.ValidatePassword(loginModel.UserName, loginModel.Password))
                {
                    ModelState.AddModelError("Password", "Wrong Password");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginModel.UserName),
                    new Claim(ClaimTypes.Role, userList.GetRole(loginModel.UserName))
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    RedirectUri = "/"
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction("Index", "Event");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("UserName", ex.Message);
                return View();
            }
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Event");
        }

        public IActionResult Profile(User model)
        {

            var selectedUsers = from user in userList.users
                           where user.FirstName == User.Identity.Name 
                           select user;

            model = selectedUsers.ElementAt(0);

            model.Tickets = UserTickets(model);

            ViewBag.Role = model.Role;

            ViewBag.Avatar = model.Avatar;

            return View("Profile", model);
        }

        public List<Ticket> UserTickets(User model)
        {
            var selectedTickets = from ticket in ticketList.tickets
                                  where ticket.Seller == model.FirstName
                                  select ticket;

            return selectedTickets.ToList();
        }
    }
}
