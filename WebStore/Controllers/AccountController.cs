using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Indentity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
        }
        public IActionResult Register() => View(new RegisterUserViewModel());
        [HttpPost]
        public IActionResult Register(RegisterUserViewModel Model)
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login() => View();

        public IActionResult Logout() => RedirectToAction("Index", "Home");

        public IActionResult AccessDenied() => View();
    }
}
