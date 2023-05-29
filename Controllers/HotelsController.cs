using HotelBookingSample.Models;
using HotelBookingSample.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingSample.Controllers
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
            var hotelData = new List<object>();

            foreach (var hotel in hotels)
            {
                var availableRoomCount = _roomRepository.GetAvailableRoomCountByHotelId(hotel.Id);
                var hotelInfo = new
                {
                    HotelName = hotel.Name,
                    HotelLocation = hotel.Location,
                    AvailableRoomCount = availableRoomCount
                };
                hotelData.Add(hotelInfo);
            }

            return Ok(hotelData);
        }


        [HttpGet("{id}")]
        
        public IActionResult GetHotel(int id)
        {
            var hotel = _hotelRepository.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }

            var roomDetails = _roomRepository.GetRoomsByHotelId(id)
                .Select(r => new
                {
                    RoomNumber = r.Number,
                    Price = r.Price,
                    Type = r.RoomType,
                    Occupancy = r.RoomOccupancy,
                    Status = r.Availability ? "Ready to Occupy" : "Not Available"
                })
                .ToList();

            var result = new
            {
                HotelName = hotel.Name,
                HotelLocation = hotel.Location,
                Rooms = roomDetails
            };

            return Ok(result);
        }



        [HttpPost]
        public IActionResult CreateHotel(Hotel hotel)
        {
            _hotelRepository.AddHotel(hotel);
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateHotel(int id, [Bind(nameof(Hotel.Name), nameof(Hotel.Location))] Hotel hotel)
        {
            var existingHotel = _hotelRepository.GetHotelById(id);
            if (existingHotel == null)
            {
                return NotFound();
            }

            existingHotel.Name = hotel.Name;
            existingHotel.Location = hotel.Location;

            _hotelRepository.UpdateHotel(existingHotel);

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

        [HttpGet("Filter Hotels Based On Location")]
        public IActionResult FilterHotels(string location)
        {
            var filteredHotels = _hotelRepository.FilterHotels(location);
            return Ok(filteredHotels);
        }

        [HttpGet("AvailableRoomsByHotelId")]
        public IActionResult GetAvailableRoomsByHotelId(int id)
        {
            var hotel = _hotelRepository.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }

            var availableRooms = _roomRepository.GetRoomsByHotelId(id).Where(r => r.Availability);

            var hotelWithAvailableRooms = new
            {
                HotelName = hotel.Name,
                AvailableRooms = availableRooms.Select(r => new
                {
                    RoomNumber = r.Number,
                    RoomType = r.RoomType,
                    Price = r.Price
                    
                })
            };

            return Ok(hotelWithAvailableRooms);
        }



    }
}
