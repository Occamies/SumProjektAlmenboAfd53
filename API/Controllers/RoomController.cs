using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTOs;
using API.Contexts;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    // Reference tol EF Core DbContext.
    private readonly BookingContext _context;

    // Dependency Injection -> ASP.NET Core leverer automatisk BookingContext.
    public RoomController(BookingContext context)
    {
        _context = context;
    }
    
    // Get alle rum
    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var rooms = await _context.Rooms
            .Select(room => new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Location = room.Location
            })
            .ToListAsync();

        return Ok(rooms);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRoom(CreateRoomDto dto)
    {
        var room = new Room(dto.Name, dto.Location);

        _context.Rooms.Add(room);
        
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(
            nameof(GetRoomById),
            new { id = room.Id },
            room);
    }
    
    // Get by Id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoomById(Guid id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            return NotFound();
        }

        var roomDto = new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            Location = room.Location
        };

        return Ok(roomDto);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRoom(
        Guid id,
        UpdateRoomDto dto)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
        {
            return NotFound();
        }

        // Update() er nedarvet fra Item-klassen.
        room.Update(dto.Name, dto.Location);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // Delete
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRoom(Guid id)
    {
        var room = await _context.Rooms.FindAsync(id);
        
        if (room == null)
        {
            return NotFound();
        }
        
        _context.Rooms.Remove(room);
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
}