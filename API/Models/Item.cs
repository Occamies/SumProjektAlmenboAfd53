using System;
using System.Collections.Generic;

namespace SUM.Model;

public class Item
{
    private Guid Id { get; set; }
    private string Name { get; set; }
    private string Location { get; set; }
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
}