
namespace API.DTOs;

public class ExtraEquipmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public double Deposit { get; set; }
    public string Status { get; set; }
}