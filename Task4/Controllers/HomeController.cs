using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task4.Data;
using Task4.Interfaces;
using Task4.Models;

namespace Task4.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUsersService _usersService;

        public HomeController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> IndexDelete(List<string> usersId)
        {
            await _usersService.DeleteUsersAsync(usersId);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> IndexBlock(List<string> usersId)
        {
            await _usersService.UpdateUsersStatusAsync(usersId, Enums.StatusEnum.Block);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> IndexUnblock(List<string> usersId)
        {
            await _usersService.UpdateUsersStatusAsync(usersId, Enums.StatusEnum.Unblock);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Index()
        {
            var users = await _usersService.GetAllAsync();

            return View(new UserViewModel { Users = users});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}