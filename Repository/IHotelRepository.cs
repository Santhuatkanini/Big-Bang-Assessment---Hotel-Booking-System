public interface IHotelRepository
{
    IEnumerable<Hotel> GetHotels();
    Hotel GetHotelById(int id);
    void AddHotel(Hotel hotel);
    void UpdateHotel(Hotel hotel);
    void DeleteHotel(Hotel hotel);
    IEnumerable<Hotel> FilterHotels(string location, decimal? priceFrom, decimal? priceTo);
    object GetHotel(int id);
}
