namespace HotelBooking.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public bool IsAvailable { get; internal set; }
    }
}
