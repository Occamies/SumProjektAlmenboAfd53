namespace API.DTOs;

public class CreateExtraEquipmentDto
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public double Deposit { get; set; }
}