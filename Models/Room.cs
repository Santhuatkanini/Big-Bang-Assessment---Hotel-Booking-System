namespace HotelBooking.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string? Number { get; set; }
        public decimal Price { get; set; }
        public bool Availability { get; set; }
        public Hotel Hotel { get; set; }
    }
}