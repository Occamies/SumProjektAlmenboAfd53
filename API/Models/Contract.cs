using System;

namespace SUM.Model;

public class Contract
{
    public Guid Id { get; set; }
    public bool SignedByUser  { get; set; }
    public bool SignedByAdmin { get; set; }
    public DateOnly Date { get; set; }

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
    
    public void Update(DateOnly date)
    {
        Date = date;
    }
}