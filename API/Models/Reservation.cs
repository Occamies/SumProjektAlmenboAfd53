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
    
    //public int ContractId { get; set; }
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
        if (code == this.Code)
        {
            this.Cancelled = true;
            Item.Reservations.Remove(this);
            return true;
        }
        throw new ArgumentException("Forkert kodeord");
    }
}