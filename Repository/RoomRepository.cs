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

    public IEnumerable<Room> GetRooms()
    {
        return _context.Rooms.ToList();
    }
}
