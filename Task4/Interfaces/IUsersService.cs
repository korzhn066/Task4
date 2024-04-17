using Task4.Entities;
using Task4.Enums;

namespace Task4.Interfaces
{
    public interface IUsersService
    {
        Task<List<ApplicationUser>> GetAllAsync();
        Task UpdateUsersStatusAsync(List<string> usersId, StatusEnum status);
        Task DeleteUsersAsync(List<string> usersId);
    }
}
