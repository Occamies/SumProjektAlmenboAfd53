using API.DTOs;
using System.Net.Http.Json;
using System.Linq;

namespace FrontendBooking.Services;

public class SelectedRoomService
{
    
    public async Task LoadRooms(HttpClient http)
    {
       Rooms = await http.GetFromJsonAsync<List<RoomDto>>("api/room") ?? new List<RoomDto>();
       
       OnChange?.Invoke();
    }
    
    public async Task LoadReservationsByRoom(HttpClient http)
    {
        if (SelectedRoom == null)
        {
            Bookings = [];
            return;
        }
        
        var AllBookings = await http.GetFromJsonAsync<List<ReservationDto>>
            ("api/reservation") ?? new List<ReservationDto>();

        Bookings = AllBookings.Where(b => b.ItemId == SelectedRoom.Id).ToList();
        
        OnChange?.Invoke();
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
    
    //TODO select ekstra equipment
    //Todo cancel reservation
    //TODO setup med kontrakt
    //TODO skrive kommentarer
    
    //TODO kaste exceptions.
}