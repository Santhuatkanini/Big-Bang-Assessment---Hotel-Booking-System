namespace HotelBooking.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
