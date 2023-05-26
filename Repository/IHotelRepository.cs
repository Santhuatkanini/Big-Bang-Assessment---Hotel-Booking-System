using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Repository
{
    public interface IHotelRepository
    {
        Task<Hotel> GetHotelById(int id);
        Task<IEnumerable<Hotel>> GetHotels();
        Task AddHotel(Hotel hotel);
        Task UpdateHotel(Hotel hotel);
        Task DeleteHotel(int id);
    }


    public class HotelRepository : IHotelRepository
    {
        private readonly DbContext _dbContext;

        public HotelRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            return await _dbContext.Set<Hotel>()
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Hotel>> GetHotels()
        {
            return await _dbContext.Set<Hotel>()
                .Include(h => h.Rooms)
                .ToListAsync();
        }

        public async Task AddHotel(Hotel hotel)
        {
            _dbContext.Set<Hotel>().Add(hotel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateHotel(Hotel hotel)
        {
            _dbContext.Entry(hotel).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteHotel(int id)
        {
            var hotel = await GetHotelById(id);
            if (hotel != null)
            {
                _dbContext.Set<Hotel>().Remove(hotel);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
