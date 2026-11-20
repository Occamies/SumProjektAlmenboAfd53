using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTOs;
using API.Contexts;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ContractController : ControllerBase
{
    // TODO: SKAL IKKE LIGGE I RUNTIME!
    private static List<Contract> contracts = [];

    [HttpPost]
    public IActionResult CreateContract(Contract contract)
    {
        contracts.Add(contract);

        return CreatedAtAction(nameof(GetByContractId), new { id = contract.Id }, contract);
    }

    [HttpGet]
    public IActionResult GetAllContracts()
    {
        return Ok(contracts);
    }

    [HttpGet("{Id}")]
    public IActionResult GetByContractId(Guid id)
    {
        var contract = contracts.FirstOrDefault(i => i.Id == id);
        
        if (contract == null)
        {
            return NotFound();
        }
        return Ok(contract);
    }

    [HttpPut("{Id}")]
    public IActionResult UpdateContract(Guid id, Contract updatedContract)
    {
        var contract = contracts.FirstOrDefault(i => i.Id == id);

        if (contract == null)
        {
            return NotFound();
        }

        contract.Update(updatedContract.Date);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteContract(Guid id)
    {
        var contract = contracts.FirstOrDefault(i => i.Id == id);
        if (contract == null)
        {
            return NotFound();
        }

        contracts.Remove(contract);
        return NoContent();
    }
}