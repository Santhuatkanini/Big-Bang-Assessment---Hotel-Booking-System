using HotelBookingSample.Models;

namespace HotelBookingSample.Repository
{
    public interface ITokenService
    {
        string GenerateToken(User user, string role);
    }
}
