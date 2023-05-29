using HotelBookingSample.Models;

namespace HotelBookingSample.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelBookingDbContext _context;
        private readonly IRoomRepository _roomRepository;
        public HotelRepository(HotelBookingDbContext context, IRoomRepository roomRepository)
        {
            _context = context;
            _roomRepository = roomRepository;
        }

        public IEnumerable<Hotel> GetHotels()
        {
            return _context.Hotels.ToList();
        }

        public Hotel GetHotelById(int id)
        {
            return _context.Hotels.Find(id);
        }

        public void AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public void UpdateHotel(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            _context.SaveChanges();
        }

        public void DeleteHotel(Hotel hotel)
        {
            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
        }


        public object GetHotel(int id)
        {
            throw new NotImplementedException();
        }

        public object FilterHotels(string location)
        {
            var query = _context.Hotels.AsQueryable();

            var filteredHotels = query.Select(h => new
            {
                HotelId = h.Id,
                HotelName = h.Name,
                HotelLocation = h.Location,
                AvailableRoomCount = _roomRepository.GetRoomsByHotelId(h.Id).Count(r => r.Availability),

            });

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(h => h.Location.Contains(location));
            }

            return query.ToList();
        }
    }
}
