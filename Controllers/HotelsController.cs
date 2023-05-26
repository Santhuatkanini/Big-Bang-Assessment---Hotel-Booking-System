using HotelBooking.Data;
using HotelBooking.Models;
using HotelBooking.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels(string location, decimal? minPrice, decimal? maxPrice)
        {
            var hotels = await _hotelRepository.GetHotels();

            // Apply filtering based on location
            if (!string.IsNullOrEmpty(location))
            {
                hotels = hotels.Where(h => h.Location.ToLower() == location.ToLower());
            }

            // Apply filtering based on price range
            if (minPrice.HasValue)
            {
                hotels = hotels.Where(h => h.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                hotels = hotels.Where(h => h.Price <= maxPrice.Value);
            }

            return Ok(hotels);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel(int id)
        {
            var hotel = await _hotelRepository.GetHotelById(id);
            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel(Hotel hotel)
        {
            await _hotelRepository.AddHotel(hotel);
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
                return BadRequest();

            await _hotelRepository.UpdateHotel(hotel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _hotelRepository.DeleteHotel(id);
            return NoContent();
        }

        [HttpGet("{id}/rooms/count")]
        public async Task<IActionResult> GetAvailableRoomsCount(int id)
        {
            var hotel = await _hotelRepository.GetHotelById(id);
            if (hotel == null)
                return NotFound();

            int count = hotel.Rooms.Count(r => r.IsAvailable);
            return Ok(count);
        }

    }

}
