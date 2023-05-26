using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;

        public HotelController(IHotelRepository hotelRepository, IRoomRepository roomRepository)
        {
            _hotelRepository = hotelRepository;
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetHotels()
        {
            var hotels = _hotelRepository.GetHotels();
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public IActionResult GetHotel(int id)
        {
            var hotel = _hotelRepository.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [HttpPost]
        public IActionResult CreateHotel(Hotel hotel)
        {
            _hotelRepository.AddHotel(hotel);
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }
            _hotelRepository.UpdateHotel(hotel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var hotel = _hotelRepository.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            _hotelRepository.DeleteHotel(hotel);
            return NoContent();
        }

        [HttpGet("filter")]
        public IActionResult FilterHotels(string location, decimal? priceFrom, decimal? priceTo)
        {
            var filteredHotels = _hotelRepository.FilterHotels(location, priceFrom, priceTo);
            return Ok(filteredHotels);
        }

        [HttpGet("{id}/rooms/count")]
        public IActionResult GetRoomCount(int id)
        {
            var hotel = _hotelRepository.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }

            int roomCount = _roomRepository.GetRoomsByHotelId(id).Count();
            return Ok(roomCount);
        }
    }

}
