using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTOs;
using API.Contexts;

namespace API.Controllers;

[ApiController]


[Route("api/[controller]")]
public class ExtraEquipmentController : ControllerBase
{
    private readonly BookingContext _context;
    
    public ExtraEquipmentController(BookingContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExtraEquipment()
    {
        var equipment = await _context.ExtraEquipments.ToListAsync();

        return Ok(equipment);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetExtraEquipmentById(Guid id)
    {
        var equipment = await _context.ExtraEquipments.FindAsync(id);

        if (equipment == null)
        {
            return NotFound();
        }

        return Ok(equipment);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateExtraEquipment(
        CreateExtraEquipmentDto dto)
    {

        var equipment = new ExtraEquipment(
            dto.Name,
            dto.Location,
            dto.Deposit
        );
        
        _context.ExtraEquipments.Add(equipment);
        
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(
            nameof(GetExtraEquipmentById),
            new { id = equipment.Id },
            equipment);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateExtraEquipment(
        Guid id,
        UpdateExtraEquipmentDto dto)
    {
        var equipment = await _context.ExtraEquipments.FindAsync(id);
        
        if (equipment == null)
        {
            return NotFound();
        }

        equipment.Update(dto.Name, dto.Location);
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteExtraEquipment(Guid id)
    {
        var equipment = await _context.ExtraEquipments.FindAsync(id);

        if (equipment == null)
        {
            return NotFound();
        }
        
        _context.ExtraEquipments.Remove(equipment);
        
        await _context.SaveChangesAsync();

        return NoContent();
    }
}