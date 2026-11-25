using System;
using System.Collections.Generic;

namespace API.Models;

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

    // hjælpemetode til CreateReservation. Sørger for at der ikke kan opstå dobbeltbooking.
    // Guid? ignoreReservationId bruges til at sørge for at der ikke bliver konflikt med
    // den tror der er allerede booket på grund af den reservation man er ved at oprette
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
        if (person is null)
        {
            throw new ArgumentException("Bruger mangler");
        }

        if (startDate == default)
        {
            throw new ArgumentException("Startdato skal tilføjes");
        }

        if (endDate == default)
        {
            throw new ArgumentException("Slutdato skal tilføjes");
        }

        if (endDate <= startDate)
        {
            throw new ArgumentException("EndDate skal være efter StartDate");
        }

        if (startDate.Date < DateTime.Today)
        {
            throw new ArgumentException("StartDate skal tidligst være idag");
        }

        if (!IsAvailable(startDate, endDate))
        {
            throw new InvalidOperationException("Reservationen er ikke tilgængelig");
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