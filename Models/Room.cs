namespace HotelBookingSample.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Number { get; set; }
        public decimal Price { get; set; }
        public bool Availability { get; set; }
        public string RoomType { get; set; }
        public int RoomOccupancy { get; set; }

        public string Amenity { get; set; }
    }
}
