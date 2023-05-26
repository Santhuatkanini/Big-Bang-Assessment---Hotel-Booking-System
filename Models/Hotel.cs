namespace HotelBooking.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
