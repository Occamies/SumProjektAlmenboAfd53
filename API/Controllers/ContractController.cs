
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTOs;
using API.Contexts;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractController : ControllerBase
{
    // Gem en reference til vores EF Core DbContext.
    // readonly betyder, at feltet ikke kan tildeles en ny værdi efter initialisering.
    private readonly BookingContext _context;
    
    // ASP.NET Core leverer automatisk BookingContext gennem Dependency Injection.
    public ContractController(BookingContext context)
    {
        _context = context;
    }
    
    // Hent all kontrakter
    [HttpGet]
    public async Task<IActionResult> GetAllContracts()
    {
        var contracts = await _context.Contracts
            .Select(c => new ContractDto
            {
                Id = c.Id,
                Date = c.Date,
                SignedByUser = c.SignedByUser,
                SignedByAdmin = c.SignedByAdmin
            })
            .ToListAsync();

        return Ok(contracts);
    }

    // Hent kontrakt ud fra Id
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetContractById(Guid id)
    {
        var contract = await _context.Contracts.FindAsync(id);

        if (contract == null)
            return NotFound();

        return Ok(new ContractDto
        {
            Id = contract.Id,
            Date = contract.Date,
            SignedByUser = contract.SignedByUser,
            SignedByAdmin = contract.SignedByAdmin
        });
    }

    // Opret kontrakt
    [HttpPost]
    public async Task<IActionResult> CreateContract(
        CreateContractDto dto)
    {
        var contract = new Contract(dto.Date);

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetContractById),
            new { id = contract.Id },
            new ContractDto
            {
                Id = contract.Id,
                Date = contract.Date,
                SignedByUser = contract.SignedByUser,
                SignedByAdmin = contract.SignedByAdmin
            });
    }
    
    // Slet kontrakt
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteContract(Guid id)
    {
        var contract = await _context.Contracts.FindAsync(id);

        if (contract == null)
            return NotFound();

        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
