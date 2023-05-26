namespace HotelBooking.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<Room> Rooms { get; set; }
        public decimal Price { get; internal set; }
    }
}
