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

    public Reservation CreateReservation(DateTime startDate, DateTime endDate, Person person)
    {
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