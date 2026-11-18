using System;
using System.Collections.Generic;

namespace SUM.Model;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public List<Reservation> Reservations { get; set; }

    public Item(string name, string location)
    {
        Id = Guid.NewGuid();
        Name = name;
        Location = location;
        Reservations = [];
    }
    
    public Reservation LastReservation()
    {
        return Reservations[Reservations.Count - 1];
    }

    private bool IsAvailable(DateTime startDate, DateTime endDate, Guid? ignoreReservationId = null)
    {
        return !Reservations.Any( r=>
            r.Id != ignoreReservationId &&
            r.StartDate <= endDate &&
            r.EndDate > startDate &&
            !r.Cancelled);
    }

    public Reservation CreateReservation(DateTime startDate, DateTime endDate, Person person)
    {
        if (endDate <= startDate)
        {
            throw new ArgumentException("EndDate skal være efter StartDate");
        }

        if (startDate.Date.CompareTo(DateTime.Today) < 0)
        {
            throw new ArgumentException("StartDate skal tidligst idag");
        }

        if (!IsAvailable(startDate, endDate))
        {
            throw new InvalidOperationException("Reservation is not available");
        }
        
        var reservation = new Reservation(startDate, endDate, this, person);
        Reservations.Add(reservation);
        return reservation;

    }

    public void Update(string name, string location)
    {
        Name = name;
        Location = location;
    }
}