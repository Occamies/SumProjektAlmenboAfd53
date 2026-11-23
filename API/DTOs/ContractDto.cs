namespace API.DTOs;

public class ContractDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public bool SignedByUser { get; set; }
    public bool SignedByAdmin { get; set; }
}