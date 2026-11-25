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
    // Gem en reference til vores EF Core DbContext.
    // readonly betyder, at feltet ikke kan tildeles en ny værdi efter initialisering.
    private readonly BookingContext _context;

    // ASP.NET Core leverer automatisk BookingContext gennem Dependency Injection.
    public ReservationController(BookingContext context)
    {
        _context = context;
    }

    // Hent alle reservationer der ikke er aflyst
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

    // Hent reservation ud fra id
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

    // Opret reservation
    [HttpPost]
    public async Task<IActionResult> CreateReservation(
        CreateReservationDto dto)
    {
        if (dto.Contract is null)
            return BadRequest("Regler og vilkår skal accepteres.");

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

        // Kontrakt oprettes og underskrives af bruger
        var contract = new Contract(dto.Contract.Date);
        contract.SignUser();
        // Vedhæfter kontrakt til reservationen
        reservation.AttachContract(contract);

        // Fortæller EF Core at reservationen og kontrakten er ny.
        _context.Entry(reservation).State = EntityState.Added;
        _context.Entry(contract).State = EntityState.Added;

        // Reservationen er allerede tilføjet til Item.Reservations.
        // EF Core tracker den gennem det indlæste Item.
        await _context.SaveChangesAsync();

        var result = new ReservationDto
        {
            Id = reservation.Id,
            ItemId = item.Id,
            PersonId = person.Id,
            StartDate = reservation.StartDate,
            EndDate = reservation.EndDate,
            Contract = new ContractDto
            {
                Id = contract.Id,
                Date = contract.Date,
                SignedByUser = contract.SignedByUser,
                SignedByAdmin = contract.SignedByAdmin
            }
        };


        return CreatedAtAction(
            nameof(GetReservationById),
            new { id = reservation.Id },
            result);
    }

    // Aflys reservation
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