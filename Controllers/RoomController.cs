using HotelBookingSample.Models;
using HotelBookingSample.Repository;
using Microsoft.AspNetCore.Mvc;


namespace HotelBookingSample.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public RoomController(IRoomRepository roomRepository, IBookingRepository bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;

        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _roomRepository.GetRooms();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = _roomRepository.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }


        [HttpGet("Filter Hotels By Amenity")]
        public IActionResult GetRoomsByAmenity(string amenity)
        {
            var rooms = _roomRepository.GetRoomsByAmenity(amenity);
            return Ok(rooms);
        }


        [HttpPost]
        public IActionResult CreateRoom(Room room)
        {
            _roomRepository.AddRoom(room);
            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

       
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }
            _roomRepository.UpdateRoom(room);
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _roomRepository.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            _roomRepository.DeleteRoom(room);
            return NoContent();
        }

        [HttpGet("FilterByPriceRange")]
        public IActionResult FilterRoomsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var rooms = _roomRepository.GetRooms()
                .Where(r => r.Price >= minPrice && r.Price <= maxPrice)
                .ToList();

            return Ok(rooms);
        }



        [HttpGet("{id}/bookings")]
        public IActionResult GetRoomBookings(int id)
        {
            var room = _roomRepository.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }

            var bookings = _bookingRepository.GetBookings().Where(b => b.RoomId == id);
            return Ok(bookings);
        }

      
        [HttpPost("{id}/bookings")]
        public IActionResult BookRoom(int id, Booking booking)
        {
            var room = _roomRepository.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }

            if (!room.Availability)
            {
                return BadRequest("The room is not available for booking.");
            }

            booking.RoomId = id;
            _bookingRepository.AddBooking(booking);

            room.Availability = false;
            _roomRepository.UpdateRoom(room);

            return CreatedAtAction("GetRoomBookings", new { id = room.Id }, booking);
        }

        
        [HttpDelete("bookings/{bookingId}")]
        public IActionResult CancelBooking(int bookingId)
        {
            var booking = _bookingRepository.GetBookingById(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            var room = _roomRepository.GetRoomById(booking.RoomId);
            if (room == null)
            {
                return NotFound();
            }

            _bookingRepository.DeleteBooking(booking);

            room.Availability = true;
            _roomRepository.UpdateRoom(room);

            return NoContent();
        }
    }

}




