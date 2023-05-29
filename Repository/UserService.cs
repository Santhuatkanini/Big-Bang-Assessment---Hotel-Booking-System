using HotelBookingSample.Models;
using HotelBookingSample.Repository;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HotelBookingSample.Repository
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CreateUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> AssignRoleAsync(User user, string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var newRole = new Role { Name = roleName };
                var createRoleResult = await _roleManager.CreateAsync(newRole);
                if (!createRoleResult.Succeeded)
                {
                    return false;
                }
            }

            var assignRoleResult = await _userManager.AddToRoleAsync(user, roleName);
            return assignRoleResult.Succeeded;
        }
    }
}
