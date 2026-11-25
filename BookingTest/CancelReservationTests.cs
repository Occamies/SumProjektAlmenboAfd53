using System;
using API.Models;
using Xunit;

namespace BookingTest;

public class CancelReservationTests
{
    // Tjekker om resevarion kan annulleres Test
    [Fact]
    public void CancelReservationSunshineTest()
    {
        var item = new Item("testName", "testLocation");

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testperson", "persontest@email.lol", "testvej", "12345678");
        
        item.CreateReservation(startDate, endDate, testPerson);
        var sut = item.LastReservation();

        var result = sut.CancelReservation("1234");

        Assert.True(result);
    }
    
    // Tjekker om kodeord er forkert Test
    [Fact]
    public void wrongPassWordTest()
    {
        var item = new Item("testName", "testLocation");

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testperson", "persontest@email.lol", "testvej", "12345678");
        
        item.CreateReservation(startDate, endDate, testPerson);
        var sut = item.LastReservation();
        
        var exeption = Assert.Throws<ArgumentException>(() => sut.CancelReservation("0000"));

        Assert.Equal("Forkert kodeord", exeption.Message);
    }
    
    // Tjekker om kodeord er null
    [Fact]
    public void NullPassWordTest()
    {
        var item = new Item("testName", "testLocation");

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testperson", "persontest@email.lol", "testvej", "12345678");
        
        item.CreateReservation(startDate, endDate, testPerson);
        var sut = item.LastReservation();
        
        var exeption = Assert.Throws<ArgumentException>(() => sut.CancelReservation(null));

        Assert.Equal("Indtast kode", exeption.Message);
    }
    
    // Tjekker om kodeord er null
    [Fact]
    public void ReservationAlreadyCancelledTest()
    {
        var item = new Item("testName", "testLocation");

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testperson", "persontest@email.lol", "testvej", "12345678");
        
        item.CreateReservation(startDate, endDate, testPerson);
        var sut = item.LastReservation();
        sut.CancelReservation("1234");
        
        var exeption = Assert.Throws<ArgumentException>(() => sut.CancelReservation("1234"));

        Assert.Equal("Reservationen er allerede aflyst", exeption.Message);
    }   
}