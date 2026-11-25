namespace API.DTOs;

public class RoomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public List<ExtraEquipmentDto> Inventory { get; set; } = [];
}