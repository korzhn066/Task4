using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task4.Entities;
using Task4.Interfaces;
using Task4.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task4.Controllers
{
    public class AccountController : Controller
    {
        private readonly IApplicationAuthenticateService _applicationAuthenticateService;

        public AccountController(IApplicationAuthenticateService applicationAuthenticateService)
        {
            _applicationAuthenticateService = applicationAuthenticateService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _applicationAuthenticateService.LogOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _applicationAuthenticateService.RegisterAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _applicationAuthenticateService.LoginAsync(model);

                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
            }

            return View(model);
        }
    }
}
