using HotelBookingSample.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSample.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelBookingDbContext _context;

        public RoomRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetRoomsByHotelId(int hotelId)
        {
            return _context.Rooms.Where(r => r.HotelId == hotelId).ToList();
        }

        public Room GetRoomById(int id)
        {
            return _context.Rooms.Find(id);
        }


        public IEnumerable<object> GetRoomsByAmenity(string amenity)
        {
            var lowercasedAmenity = amenity.ToLowerInvariant();

            return _context.Rooms
                
                .ToList()
                .Where(r => r.Amenity.ToLowerInvariant() == lowercasedAmenity)
                .Select(r => new
                {
                    Price = r.Price,
                    Type = r.RoomType,
                    Occupancy = r.RoomOccupancy
                })
                .ToList();
        }

        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
            _context.SaveChanges();
        }

        public void DeleteRoom(Room room)
        {
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }

        

        public async Task<List<Room>> GetAvailableRooms()
        {
            try
            {
                var availableRooms = await _context.Rooms
                    .Where(r => r.Availability.Equals("Available"))
                    .ToListAsync();

                return availableRooms;
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        public object GetAvailableRoomCountByHotelId(int hotelId)
        {
            return _context.Rooms.Count(r => r.HotelId == hotelId && r.Availability);
        }

       

        public object GetAvailableRoomCount(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetRooms()
        {
            return _context.Rooms.ToList();
        }

    }
}