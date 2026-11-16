using Microsoft.AspNetCore.Mvc;
using SUM.Model;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResevationController : ControllerBase
{
    private static List<Reservation> reservations = [];
    
    [HttpPost]
    public IActionResult CreateReservation(Reservation reservation)
    {
        reservations.Add(reservation);

        return CreatedAtAction(nameof(GetByResevationId), new { id = reservation.Id }, reservation);
    }

    [HttpGet]
    public IActionResult GetAllReservations()
    {
        return Ok(reservations);
    }

    [HttpGet("{Id}")]
    public IActionResult GetByResevationId(Guid id)
    {
        var resevation = reservations.FirstOrDefault(i => i.Id == id);

        if (resevation == null)
        {
            return NotFound();
        }
        return Ok(resevation);
    }
    
    [HttpPut("{Id}")]
    public IActionResult UpdateItem(Guid id, string code)
    {
        var reservation = reservations.FirstOrDefault(i => i.Id == id);

        if (reservation == null)
        {
            return NotFound();
        }
        
        reservation.CancelReservation(code);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteItem(Guid id)
    {
        var reservation = reservations.FirstOrDefault(i => i.Id == id);
        if (reservation == null)
        {
            return NotFound();
        }
        reservations.Remove(reservation);
        return NoContent();
    }
    
}