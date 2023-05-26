using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomRepository _roomRepository;

    public RoomController(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    [HttpGet]
    public IActionResult GetRooms()
    {
        IEnumerable<Room> rooms = _roomRepository.GetRooms();
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
}


