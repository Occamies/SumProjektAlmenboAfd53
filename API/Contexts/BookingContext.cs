using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Contexts;

public class BookingContext : DbContext
{
    public BookingContext(DbContextOptions<BookingContext> options) : base(options){}
    
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<ExtraEquipment>  ExtraEquipments => Set<ExtraEquipment>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Person> People => Set<Person>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<Room> Rooms => Set<Room>();
}