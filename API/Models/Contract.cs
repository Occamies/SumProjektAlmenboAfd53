namespace SUM.Model;

public class Contract
{
    private Guid Id { get; set; }
    private bool SignedByUser  { get; set; }
    private bool SignedByAdmin { get; set; }
    private DateOnly Date { get; set; }

    public Contract(DateOnly date)
    {
        Id = Guid.NewGuid();
        SignedByUser = false;
        SignedByAdmin = false;
        Date = date;
    }

    public void SignUser()
    {
        SignedByUser = true;
    }

    public void SignAdmin()
    {
        SignedByAdmin = true;
    }
}