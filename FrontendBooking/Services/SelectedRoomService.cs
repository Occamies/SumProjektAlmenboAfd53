using API.DTOs;
using System.Net.Http.Json;
using System.Linq;

namespace FrontendBooking.Services;

public class SelectedRoomService
{
    //Henter alle fællesrum fra API/Database.
    public async Task LoadRooms(HttpClient http)
    {
       Rooms = await http.GetFromJsonAsync<List<RoomDto>>("api/room") ?? new List<RoomDto>();
       
       //Giver besked om, at data er blevet opdateret.
       OnChange?.Invoke();
    }
    //Henter reservationer og filtrerer dem efter det valgte fællesrum.
    public async Task LoadReservationsByRoom(HttpClient http)
    {
        //Fejlsikring: Kontrollerer, om der er valgt et fællesrum.
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
    
    //Kontrollerer, om et fællesrum er valgt.
    public RoomDto? SelectedRoom { get; private set; }

    //Kontrollerer, om et fællesrum er valgt.
    public bool HasRoom => SelectedRoom != null;

    //Event, der giver besked til komponenter om ændringer.
    public event Action? OnChange;

    //Gemmer brugerens valgte fællesrum.
    public void SelectRoom(RoomDto room)
    {
        SelectedRoom = room;
        
        OnChange?.Invoke();
    }

}