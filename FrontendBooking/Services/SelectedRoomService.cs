using API.DTOs;
using System.Net.Http.Json;

namespace FrontendBooking.Services;

public class SelectedRoomService
{
    
    public async Task LoadRoomes(HttpClient http)
    {
       Rooms = await http.GetFromJsonAsync<List<RoomDto>>("api/room") ?? new List<RoomDto>();
    }

    //Todo tilgå det rigtige api endpoint
    public async Task LoadReservationsByRoom(HttpClient http)
    {
        Bookings = await http.GetFromJsonAsync<List<ReservationDto>>("api/reservation") ?? new List<ReservationDto>();
    }

    public List<RoomDto> Rooms { get; set; } = [];

    public List<ReservationDto> Bookings { get; set; } = [];
    
    
    public RoomDto? SelectedRoom { get; private set; }

    public bool HasRoom => SelectedRoom != null;

    public event Action? OnChange;

    public void SelectRoom(RoomDto room)
    {
        SelectedRoom = room;
        
        OnChange?.Invoke();
    }
}