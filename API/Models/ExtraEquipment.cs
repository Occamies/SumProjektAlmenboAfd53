namespace API.Models;

public class ExtraEquipment : Item
{
    public double Deposit { get; set; }
    // Bruges til at vurdere om der er blevet betalt for depositum
    public DepositState Status { get; set; }

    public ExtraEquipment(string name, string location, double deposit) : base(name, location)
    {
        Deposit = deposit;
        Status = DepositState.Outstanding;
    }
    public ExtraEquipment(string name, string location) : base(name, location)
    {
        Deposit = 0.0;
        Status = DepositState.Outstanding;
    }
}