using System;

namespace SUM.Model;

public class Reservation
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Code { get; set; }
    public Item Item  { get; set; }
    public Contract? Contract { get; set; }
    public Person Person { get; set; }
    public bool Cancelled { get; set; }

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

    public void CancelReservation(string code)
    {
        if (code.Equals(Code))
        {
            Cancelled = false;
        }
    }
    
}