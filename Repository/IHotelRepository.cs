using HotelBookingSample.Models;

namespace HotelBookingSample.Repository
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> GetHotels();
        Hotel GetHotelById(int id);
        void AddHotel(Hotel hotel);
        void UpdateHotel(Hotel hotel);
        void DeleteHotel(Hotel hotel);

        object GetHotel(int id);
        object FilterHotels(string location);
    }
}