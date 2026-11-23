namespace API.DTOs;

public class CreateReservationDto
{
    public Guid ItemId { get; set; }
    public Guid PersonId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}