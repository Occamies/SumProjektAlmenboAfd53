
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTOs;
using API.Contexts;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly BookingContext _context;
    
    public ReservationController(BookingContext context)
    {
        _context = context;
    }

    // Hent alle
    [HttpGet]
    public async Task<IActionResult> GetAllReservations()
    {
        var reservations = await _context.Reservations
            .Where(r => !r.Cancelled)
            .Select(r => new ReservationDto
            {
                Id = r.Id,
                ItemId = r.ItemId,
                PersonId = r.PersonId,
                StartDate = r.StartDate,
                EndDate = r.EndDate
            })
            .ToListAsync();

        return Ok(reservations);
    }

    // Hent ud fra id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetReservationById(Guid id)
    {
        var reservation = await _context.Reservations
            .Where(r => r.Id == id && !r.Cancelled)
            .Select(r => new ReservationDto
            {
                Id = r.Id,
                ItemId = r.ItemId,
                PersonId = r.PersonId,
                StartDate = r.StartDate,
                EndDate = r.EndDate

            })
            .FirstOrDefaultAsync();

        if (reservation == null)
            return NotFound();

        return Ok(reservation);
    }

    // Create
    [HttpPost]
    public async Task<IActionResult> CreateReservation(
        CreateReservationDto dto)
    {
        var item = await _context.Items
            .Include(i => i.Reservations)
            .FirstOrDefaultAsync(i => i.Id == dto.ItemId);

        if (item == null)
            return NotFound("Item blev ikke fundet.");
        
        var person = await _context.People.FindAsync(dto.PersonId);

        if (person == null)
            return NotFound("Person blev ikke fundet.");

        Reservation reservation;

        
        try
        {
            DateTime startDate = DateTime.SpecifyKind(
                dto.StartDate, DateTimeKind.Utc);
            DateTime endDate = DateTime.SpecifyKind(
                dto.EndDate, DateTimeKind.Utc);
            
            
            reservation = item.CreateReservation(
                startDate,
                endDate,
                person);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        
        // Fortæller EF Core at reservationen er ny.
        _context.Entry(reservation).State = EntityState.Added;
        
        // Reservationen er allerede tilføjet til Item.Reservations.
        // EF Core tracker den gennem det indlæste Item.
        await _context.SaveChangesAsync();

        var result = new ReservationDto
        {
            Id = reservation.Id,
            ItemId = item.Id,
            PersonId = person.Id,
            StartDate = reservation.StartDate,
            EndDate = reservation.EndDate
        };

        return CreatedAtAction(
            nameof(GetReservationById),
            new { id = reservation.Id },
            result);
    }

    // Annuler? slet? hvad gør vi her??
    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> CancelReservation(
        Guid id, CancelReservationDto dto)
    {
        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(r => r.Id == id && !r.Cancelled);

        if (reservation == null)
            return NotFound();

        try
        {
            reservation.CancelReservation(dto.Code);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
