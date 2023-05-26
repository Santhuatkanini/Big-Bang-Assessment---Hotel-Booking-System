public class HotelRepository : IHotelRepository
{
    private readonly HotelBookingDbContext _context;

    public HotelRepository(HotelBookingDbContext context)
    {
        _context = context;
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

    public IEnumerable<Hotel> FilterHotels(string location, decimal? priceFrom, decimal? priceTo)
    {
        var query = _context.Hotels.AsQueryable();

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(h => h.Location.Contains(location));
        }

        if (priceFrom.HasValue)
        {
            query = query.Where(h => h.Rooms.Any(r => r.Price >= priceFrom.Value));
        }

        if (priceTo.HasValue)
        {
            query = query.Where(h => h.Rooms.Any(r => r.Price <= priceTo.Value));
        }

        return query.ToList();
    }

    public object GetHotel(int id)
    {
        throw new NotImplementedException();
    }


}
