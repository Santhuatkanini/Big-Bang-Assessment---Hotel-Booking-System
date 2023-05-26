public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    
    public ICollection<Room> Rooms { get; set; }

    public int AvailableRoomCount
    {
        get
        {
            // Check if Rooms is null or empty
            if (Rooms == null || !Rooms.Any())
                return 0;

            // Calculate the available room count based on the availability of rooms
            return Rooms.Count(room => room.Availability);
        }
    }
}
