using HotelBookingSample.Models;

namespace HotelBookingSample.Repository
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRoomsByHotelId(int hotelId);
        IEnumerable<Room> GetRooms();
        IEnumerable<object> GetRoomsByAmenity(string amenity);
        Room GetRoomById(int id);
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);
        Task<List<Room>> GetAvailableRooms();
        object GetAvailableRoomCountByHotelId(int id);
        object GetAvailableRoomCount(int id);
    }
}