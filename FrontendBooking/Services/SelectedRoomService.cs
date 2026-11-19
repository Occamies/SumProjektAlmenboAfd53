namespace FrontendBooking.Services;

public class SelectedRoomService
{
    public List<string> Rooms { get; } = new()
    {
        "1E", "1F", "1G", "1H"
    };

    public string? SelectedRoom { get; private set; }

    public bool HasRoom => SelectedRoom != null;

    public event Action? OnChange;

    public void SelectRoom(string room)
    {
        SelectedRoom = room;
        OnChange?.Invoke();
    }
}