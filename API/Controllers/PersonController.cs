using Microsoft.AspNetCore.Mvc;
using SUM.Model;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private static List<Person> people = [];

    [HttpPost]
    public IActionResult CreatePerson(Person person)
    {
        people.Add(person);

        return CreatedAtAction(
            nameof(GetByPersonId),
            new { id = person.Id },
            person);
    }

    [HttpGet]
    public IActionResult GetAllPeople()
    {
        return Ok(people);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetByPersonId(Guid id)
    {
        var person = people.FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdatePerson(Guid id, Person updatedPerson)
    {
        var person = people.FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        person.Update(updatedPerson.Name, updatedPerson.Email, updatedPerson.Address, updatedPerson.Phone);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePerson(Guid id)
    {
        var person = people.FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            return NotFound();
        }

        people.Remove(person);

        return NoContent();
    }
}