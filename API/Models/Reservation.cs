namespace SUM.Model;

public class Reservation
{
    private Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    private DateTime EndDate { get; set; }
    private string Code { get; set; }
    private Item Item  { get; set; }
    private Contract? Contract { get; set; }
    private Person Person { get; set; }
    private bool Cancelled { get; set; }

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

    public bool CancelReservation(string code)
    {
        return false;
    }
}