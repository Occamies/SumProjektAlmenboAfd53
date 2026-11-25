using System;

namespace API.Models;

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

    // Bruges til at underskrives af bruger
    public void SignUser()
    {
        SignedByUser = true;
    }

    // Bruges til at underskrives af admin
    // Ikke implementeret endnu
    public void SignAdmin()
    {
        SignedByAdmin = true;
    }
}