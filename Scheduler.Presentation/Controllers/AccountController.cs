using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Scheduler.BL;
using Scheduler.Models;
using Scheduler.Models.Enums;
using Scheduler.Presentation.ViewModel;

namespace Scheduler.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly Users users;

        public AccountController(Users users)
        {
            this.users = users;
        }

        public IActionResult Login()
        {
            ViewBag.Title = "Sign In";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewBag.Title = "Sign In";
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await users.Authenticate(model.UserName, model.Password);
                    await AssignCredentials(user);
                    return RedirectToAction("Index", "Schedule");
                }
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Register()
        {
            ViewBag.Title = "Register";
            ViewBag.ImageUrl = "/images/puntualidad.jpg";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                ViewBag.Title = "Register";
                ViewBag.ImageUrl = "/images/puntualidad.jpg";
                if (ModelState.IsValid)
                {
                    var user = new User
                    {
                        Username = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        UserCategoryType = UserCategoryType.User
                    };
                    await users.CreateUser(user);
                    return RedirectToAction("Login");
                }
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
        }

        private async Task AssignCredentials(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Sid, user.UserID.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}