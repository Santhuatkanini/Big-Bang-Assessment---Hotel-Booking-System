using HotelBookingSample.Models;

namespace HotelBookingSample.Repository
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> CreateUserAsync(User user, string password);
        Task<bool> AssignRoleAsync(User user, string roleName);
    }
}
