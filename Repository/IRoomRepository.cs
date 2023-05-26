public interface IRoomRepository
{
    IEnumerable<Room> GetRoomsByHotelId(int hotelId);
    IEnumerable<Room> GetRooms();
    Room GetRoomById(int id);
    void AddRoom(Room room);
    void UpdateRoom(Room room);
    void DeleteRoom(Room room);
}
