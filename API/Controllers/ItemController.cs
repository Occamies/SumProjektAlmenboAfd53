using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTOs;
using API.Contexts;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    // TODO: SKAL IKKE LIGGE I RUNTIME!
    private static List<Item> items = [];
    
    private readonly BookingContext _BookingContext;
    public ItemController(BookingContext bookingContext)
    {
        _BookingContext = bookingContext;
    }
    
    // TODO:
    // GetRooms
    // PostCreateBooking
    
    [HttpPost] 
    public async Task<IActionResult> CreateItem(Item item)
    {
        items.Add(item);
        //_BookingContext.Items.Add(item);
        //await _BookingContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByItemsId), new {id = item.Id}, item);
    }
    
    [HttpGet]
    public IActionResult GetAllItems()
    {
        //return Ok(_BookingContext.Items.ToList());
        return Ok(items);
    }

    [HttpGet("{Id:guid}")]
    public IActionResult GetByItemsId(Guid id)
    {
        //var item = _BookingContext.Items.FirstOrDefault(i => i.Id == id);
        var item = items.FirstOrDefault(i => i.Id == id);

        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> UpdateItem(Guid id, Item updatedItem)
    {
        var item = _BookingContext.Items.FirstOrDefault(i => i.Id == id);
        //var item = items.FirstOrDefault(i => i.Id == id);

        if (item == null)
        {
            return NotFound();
        }
        
        item.Update(updatedItem.Name, updatedItem.Location);
        await _BookingContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        //var item = _BookingContext.Items.FirstOrDefault(i => i.Id == id);
        var item = items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        
        //_BookingContext.Items.Remove(item);
        items.Remove(item);
        
        //await _BookingContext.SaveChangesAsync();
        return NoContent();
    }


}