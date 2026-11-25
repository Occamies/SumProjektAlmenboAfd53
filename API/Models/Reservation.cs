using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey(nameof(Id))]
public class Reservation
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    private string Code { get; set; }
    public bool Cancelled { get; set; }
    
    public Guid ItemId { get; set; }
    public Item Item  { get; set; }
    
    public Guid? ContractId { get; set; }
    public Contract? Contract { get; set; }
    
    public Guid PersonId { get; set; }
    public Person Person { get; set; }

    internal Reservation(DateTime startDate, DateTime endDate, Item item, Person person)
    {
        Id = Guid.NewGuid();
        StartDate = startDate;
        EndDate = endDate;
        Code = "1234"; //TODO Generate this!
        Item = item;
        Contract = null;
        Person = person;
        Cancelled = false;
    }
    
    internal Reservation(DateTime startDate, DateTime endDate, Guid itemId, Guid personId)
    {
        Id = Guid.NewGuid();
        StartDate = startDate;
        EndDate = endDate;
        Code = "1234"; //TODO Generate this!
        ItemId = itemId;
        Contract = null;
        PersonId = personId;
        Cancelled = false;
    }

    public bool CancelReservation(string code)
    {
        if (Cancelled)
        {
            throw new ArgumentException("Reservationen er allerede aflyst");
        }

        if (code == null)
        {
            throw new ArgumentException("Indtast kode");
        }
        
        if (code != Code)
        {
            throw new ArgumentException("Forkert kodeord");
        }

        Cancelled = true;
        return true;
    }
    
    public void AttachContract(Contract contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        if (Contract is not null)
            throw new InvalidOperationException("Reservationen har allerede en kontrakt");

        Contract = contract;
    }
}