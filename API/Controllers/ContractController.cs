
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
    private readonly BookingContext _context;
    
    public ContractController(BookingContext context)
    {
        _context = context;
    }
    
    // Hent all
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

    // Hent ud fra Id
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

    // OPret
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

    // OPdater
    // hvad er vores overjelser om at opdatere en booking? er det noget vi tilader?
    // hvis ja skal logikken så være her eller på item?
    
    // Delete
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
