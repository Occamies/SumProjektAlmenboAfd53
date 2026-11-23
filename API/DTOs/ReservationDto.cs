namespace API.DTOs;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
    public Guid PersonId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}