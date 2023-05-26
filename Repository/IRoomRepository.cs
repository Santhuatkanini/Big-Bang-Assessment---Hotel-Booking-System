using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Repository
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomById(int id);
        Task<IEnumerable<Room>> GetRoomsByHotelId(int hotelId);
        Task AddRoom(Room room);
        Task UpdateRoom(Room room);
        Task DeleteRoom(int id);
    }

    public class RoomRepository : IRoomRepository
    {
        private readonly DbContext _dbContext;

        public RoomRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await _dbContext.Set<Room>()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotelId(int hotelId)
        {
            return await _dbContext.Set<Room>()
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task AddRoom(Room room)
        {
            _dbContext.Set<Room>().Add(room);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRoom(Room room)
        {
            _dbContext.Entry(room).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRoom(int id)
        {
            var room = await GetRoomById(id);
            if (room != null)
            {
                _dbContext.Set<Room>().Remove(room);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
