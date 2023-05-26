using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly HotelBookingDbContext _context;

        public RoomsController(HotelBookingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetRooms(int hotelId)
        {
            var hotel = _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefault(h => h.HotelId == hotelId);

            if (hotel == null)
                return NotFound();

            return Ok(hotel.Rooms);
        }

        [HttpGet("{roomId}")]
        public ActionResult<Room> GetRoom(int hotelId, int roomId)
        {
            var hotel = _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefault(h => h.HotelId == hotelId);

            if (hotel == null)
                return NotFound();

            var room = hotel.Rooms.FirstOrDefault(r => r.RoomId == roomId);

            if (room == null)
                return NotFound();

            return Ok(room);
        }

        [HttpPost]
        public ActionResult<Room> CreateRoom(int hotelId, Room room)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == hotelId);

            if (hotel == null)
                return NotFound();

            room.HotelId = hotelId;
            _context.Rooms.Add(room);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRoom), new { hotelId, roomId = room.RoomId }, room);
        }

        [HttpPut("{roomId}")]
        public IActionResult UpdateRoom(int hotelId, int roomId, Room updatedRoom)
        {
            var hotel = _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefault(h => h.HotelId == hotelId);

            if (hotel == null)
                return NotFound();

            var room = hotel.Rooms.FirstOrDefault(r => r.RoomId == roomId);

            if (room == null)
                return NotFound();

            room.RoomNumber = updatedRoom.RoomNumber;
            room.Price = updatedRoom.Price;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{roomId}")]
        public IActionResult DeleteRoom(int hotelId, int roomId)
        {
            var hotel = _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefault(h => h.HotelId == hotelId);

            if (hotel == null)
                return NotFound();

            var room = hotel.Rooms.FirstOrDefault(r => r.RoomId == roomId);

            if (room == null)
                return NotFound();

            _context.Rooms.Remove(room);
            _context.SaveChanges();

            return NoContent();
        }
    }

}
