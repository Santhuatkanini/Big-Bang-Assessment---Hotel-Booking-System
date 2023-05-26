using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class HotelsController : ControllerBase
    {
        private readonly HotelBookingDbContext _context;

        public HotelsController(HotelBookingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> GetHotels(string name, string address)
        {
            var query = _context.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(h => h.HotelName.Contains(name));

            if (!string.IsNullOrEmpty(address))
                query = query.Where(h => h.Address.Contains(address));

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Hotel> GetHotel(int id)
        {
            var hotel = _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefault(h => h.HotelId == id);

            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        [HttpPost]
        public ActionResult<Hotel> CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetHotel), new { id = hotel.HotelId }, hotel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHotel(int id, Hotel updatedHotel)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == id);

            if (hotel == null)
                return NotFound();

            hotel.HotelName = updatedHotel.HotelName;
            hotel.Address = updatedHotel.Address;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == id);

            if (hotel == null)
                return NotFound();

            _context.Hotels.Remove(hotel);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("count")]
        public ActionResult<int> GetHotelsCount(string name, string address)
        {
            var query = _context.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(h => h.HotelName.Contains(name));

            if (!string.IsNullOrEmpty(address))
                query = query.Where(h => h.Address.Contains(address));

            return Ok(query.Count());
        }

    }

}
