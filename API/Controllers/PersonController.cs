using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTOs;
using API.Contexts;
using Microsoft.EntityFrameworkCore;

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
        
        var person = new Person(
            dto.Name,
            dto.Email,
            dto.Address,
            dto.Phone
        );

        
        _context.People.Add(person);

        
        await _context.SaveChangesAsync();

        var personDto = new PersonDto
        {
            Id = person.Id,
            Name = person.Name,
            Email = person.Email,
            Address = person.Address,
            Phone = person.Phone
        };

        return CreatedAtAction(
            nameof(GetPersonById),
            new { id = person.Id },
            personDto);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {
        var persons = await _context.People
            .Select(person => new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                Address = person.Address,
                Phone = person.Phone
            })
            .ToListAsync();

        return Ok(persons);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPersonById(Guid id)
    {
        var person = await _context.People.FindAsync(id);

        
        if (person == null)
        {
            return NotFound();
        }

        var personDto = new PersonDto
        {
            Id = person.Id,
            Name = person.Name,
            Email = person.Email,
            Address = person.Address,
            Phone = person.Phone
        };

        return Ok(personDto);
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