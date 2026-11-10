namespace SUM.Model;

public class Item
{
    private Guid Id { get; set; }
    private string Name { get; set; }
    private string Location { get; set; }
    private List<Reservation> Reservations { get; set; }

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

    public void CreateReservation(DateTime startDate, DateTime endDate, Person person)
    {
        Reservations.Add(new Reservation(startDate, endDate, this,  person));
    }
}