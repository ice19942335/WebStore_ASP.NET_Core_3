using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Account;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginResult = await _signInManager
                .PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (loginResult.Succeeded)
            {
                _logger.LogInformation("User <{0}> successfully logged in", model.UserName);

                if (Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Username or password is incorrect");

            _logger.LogWarning("User <{0}> login error", model.UserName);

            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            var userName = User.Identity.Name;
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User <{0}> logged out", userName);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (_logger.BeginScope($"New user registration: <{model.UserName}>"))
            {
                var newUser = new User
                {
                    UserName = model.UserName
                };

                var creationResult = await _userManager.CreateAsync(newUser, model.Password);

                if (creationResult.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, false);

                    _logger.LogInformation($"User <{model.UserName}> successfully registered in system");

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in creationResult.Errors)
                    ModelState.AddModelError("", error.Description);

                _logger.LogWarning(
                    "Registration error, User: <{0}>, Errors: {1}",
                    model.UserName, 
                    string.Join(", ", creationResult.Errors.Select(err => err.Description))
                    );
            }

            return View(model);
        }

        public IActionResult AccessDenied() => View();
    }
}