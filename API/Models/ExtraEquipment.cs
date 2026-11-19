namespace API.Models;

public class ExtraEquipment : Item
{
    private double Deposit { get; set; }
    private DepositState Status { get; set; }

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