using Microsoft.AspNetCore.Mvc;
using SUM.Model;
using Microsoft.EntityFrameworkCore;
using API.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    // Gemm en reference til vores EF Core DbContext.
    // readonly betyder, at feltet ikke kan tildeles en ny værdi efter initialisering.
    private readonly BookingContext _context;

    // ASP.NET Core leverer automatisk BookingContext gennem Dependency Injection.
    public PersonController(BookingContext context)
    {
        _context = context;
    }


    // Crud funktioner -------------------------------------------------------------------------------------
    
    [HttpPost]
    public async Task<IActionResult> CreatePerson(CreatePersonDto dto)
    {
        // Opretter et Person-objekt ud fra DTO'ens oplysninger.
        var person = new Person(
            dto.Name,
            dto.Email,
            dto.Address,
            dto.Phone
        );

        // Tilføjer personen til databasen.
        _context.People.Add(person);

        // Gemmer ændringerne i PostgreSQL.
        await _context.SaveChangesAsync();

        // Returnerer 201 Created.
        return CreatedAtAction(
            nameof(GetByPersonId),
            new { id = person.Id },
            person);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAllPeople()
    {
        // Henter alle personer fra PostgreSQL.
        var people = await _context.People.ToListAsync();

        // Returnerer personerne som JSON med HTTP-status 200 OK (forhåbentligt).
        return Ok(people);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByPersonId(Guid id)
    {
        var person = await _context.People.FindAsync(id);

        // Hvis personen ikke findes, returneres HTTP 404 Not Found.
        if (person == null)
        {
            return NotFound();
        }
        
        return Ok(person);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePerson(
        Guid id,
        UpdatePersonDto dto)
    {
        // Finder personen i databasen.
        var person = await _context.People.FindAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        // Opdaterer personen med DTO'ens oplysninger.
        person.Update(
            dto.Name,
            dto.Email,
            dto.Address,
            dto.Phone
        );

        // Gemmer ændringerne.
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        var person = await _context.People.FindAsync(id);
        
        if (person == null)
        {
            return NotFound();
        }
        
        _context.People.Remove(person);
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}