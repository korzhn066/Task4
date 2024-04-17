using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task4.Data;
using Task4.Entities;
using Task4.Enums;
using Task4.Interfaces;

namespace Task4.Services
{
    public class UsersService : IUsersService
    {
        private readonly DBContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsersService(
            DBContext dbContext,
            SignInManager<ApplicationUser> signInManager) 
        { 
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        public async Task UpdateUsersStatusAsync(List<string> usersId, StatusEnum status)
        {
            foreach (var userId in usersId)
            {
                var user = await _dbContext.Users.FindAsync(userId);

                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                if (_signInManager.Context.User.Identity is null)
                    throw new ArgumentNullException(nameof(_signInManager.Context.User.Identity));

                if (user.UserName == _signInManager.Context.User.Identity.Name)
                    await _signInManager.SignOutAsync();

                user.Status = status;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task DeleteUsersAsync(List<string> usersId)
        {
            foreach (var userId in usersId)
            {
                var user = await _dbContext.Users.FindAsync(userId);

                if (user is null)
                    throw new ArgumentNullException(nameof(user));

                if (_signInManager.Context.User.Identity is null)
                    throw new ArgumentNullException(nameof(_signInManager.Context.User.Identity));

                if (user.UserName == _signInManager.Context.User.Identity.Name)
                    await _signInManager.SignOutAsync();

                _dbContext.Users.Remove(user);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
