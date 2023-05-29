using HotelBookingSample.Models;

namespace HotelBookingSample.Repository
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetBookings();
        Booking GetBookingById(int id);
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);
        object GetBookingsByHotelId(int hotelId);
    }
}
