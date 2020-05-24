using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            ViewBag.Title = "Sign In";
            if (ModelState.IsValid)
            {
                var authenticated = await users.Authenticate(model.UserName, model.Password);
                if (authenticated)
                {
                    return RedirectToAction("Index", "Schedule");
                }
                else
                {
                    return RedirectToAction("Register");
                }
            }
            return View(model);
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
    }
}