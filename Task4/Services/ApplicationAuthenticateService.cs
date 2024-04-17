using Microsoft.AspNetCore.Identity;
using Task4.Data;
using Task4.Entities;
using Task4.Enums;
using Task4.Interfaces;
using Task4.Models;

namespace Task4.Services
{
    public class ApplicationAuthenticateService : IApplicationAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DBContext _dbContext;

        public ApplicationAuthenticateService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            DBContext dBContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dBContext;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is null)
                return false;

            if (user.Status == StatusEnum.Block)
                return false;

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            user.LastLogined = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            if (result)
                await _signInManager.SignInAsync(user, false);

            return result;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser {
                Email = model.Email,
                UserName = model.UserName,
                Name = model.Name,
                Status = Enums.StatusEnum.Unblock,
                Registered = DateTime.Now,
                LastLogined = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }

            return result;
        }
    }
}
