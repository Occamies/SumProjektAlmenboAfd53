
using API.Contexts;
using API.DTOs.Item;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    // Gem en reference til vores EF Core DbContext.
    // readonly betyder, at feltet ikke kan tildeles en ny værdi efter initialisering.
    private readonly BookingContext _context;
    
    // ASP.NET Core leverer automatisk BookingContext gennem Dependency Injection.
    public ItemController(BookingContext context)
    {
        _context = context;
    }
    
    // Hent alle items
    [HttpGet]
    public async Task<ActionResult<List<ItemDto>>> GetAll()
    {
        var items = await _context.Items
            .Select(item => new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Location = item.Location
            })
            .ToListAsync();

        return Ok(items);
    }
    
    // Hent et Item ud fra dets Id.
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemDto>> GetById(Guid id)
    {
        var item = await _context.Items.FindAsync(id);

        if (item == null)
            return NotFound();

        return Ok(new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Location = item.Location
        });
    }


    // Opret item
    [HttpPost]
    public async Task<ActionResult<ItemDto>> Create(
        CreateItemDto dto)
    {
        var item = new Item(dto.Name, dto.Location);

        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        var result = new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Location = item.Location
        };

        return CreatedAtAction(
            nameof(GetById),
            new { id = item.Id },
            result);
    }
    
    // Opdaterer item
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id, UpdateItemDto dto)
    {
        var item = await _context.Items.FindAsync(id);

        if (item == null)
            return NotFound();
        
        item.Update(dto.Name, dto.Location);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Slet item
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _context.Items.FindAsync(id);

        if (item == null)
            return NotFound();

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
