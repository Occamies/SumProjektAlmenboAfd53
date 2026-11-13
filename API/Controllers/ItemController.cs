using Microsoft.AspNetCore.Mvc;
using SUM.Model;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private static List<Item> items = [];
    
    [HttpPost]
    public IActionResult CreateItem(Item item)
    {
        items.Add(item);

        return CreatedAtAction(nameof(GetByItemsId), new {id = item.Id}, item);
    }
    
    [HttpGet]
    public IActionResult GetAllItems()
    {
        return Ok(items);
    }

    [HttpGet("{Id}")]
    public IActionResult GetByItemsId(Guid id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);

        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPut("{Id}")]
    public IActionResult UpdateItem(Guid id, Item updatedItem)
    {
        var item = items.FirstOrDefault(i => i.Id == id);

        if (item == null)
        {
            return NotFound();
        }
        
        item.Update(updatedItem.Name, updatedItem.Location);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteItem(Guid id)
    {
        var item = items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound();
        }
        items.Remove(item);
        return NoContent();
    }


}