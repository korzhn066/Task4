using Microsoft.AspNetCore.Identity;
using Task4.Models;

namespace Task4.Interfaces
{
    public interface IApplicationAuthenticateService
    {
        Task LogOutAsync();
        Task<bool> LoginAsync(LoginViewModel model);
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
    }
}
