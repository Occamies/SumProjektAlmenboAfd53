using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTOs;
using API.Contexts;
using API.DTOs.Item;

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
    
    // Add to Inventory
    [HttpPost("{id:guid}/inventory")]
    public async Task<IActionResult> AddToInventory(Guid id, CreateItemDto dto)
    {
        var item = new Item(dto.Name, dto.Location);

        var room = await _context.Rooms.FindAsync(id);
        
        _context.Items.Add(item);
        room.AddToInventory(item);
        
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(
            nameof(AddToInventory),
            new { id = item.Id },
            item);
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
    
    // Get inventory of room by Id
    [HttpGet("{id:guid}/inventory")]
    public async Task<IActionResult> GetRoomInventoryById(Guid id)
    {
        var room = await _context.Rooms.Include(r => r.Inventory).FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
        {
            return NotFound();
        }

        List<ItemDto> inventory = [];

        foreach (var item in room.Inventory)
        {
            inventory.Add(new ItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Location = item.Location
            });
        }

        return Ok(inventory);
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