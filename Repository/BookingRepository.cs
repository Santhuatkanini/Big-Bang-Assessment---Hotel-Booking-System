using HotelBookingSample.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSample.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelBookingDbContext _context;

        public BookingRepository(HotelBookingDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetBookings()
        {
            return _context.Bookings.ToList();
        }


        public Booking GetBookingById(int id)
        {
            return _context.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            _context.SaveChanges();
        }


        public void DeleteBooking(Booking booking)
        {
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
        }

        public object GetBookingsByHotelId(int hotelId)
        {
            throw new NotImplementedException();
        }
    }
}
