using System;
using SUM.Model;
using Xunit;

namespace BookingTest;

public class CancelReservationTests
{
    [Fact]
    public void CancelReservationSunshineTest()
    {
        var item = new Item("testName", "testLocation");

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testSøren", "sørentest@email.lol", "søren frichsvej", "12345678");
        
        item.CreateReservation(startDate, endDate, testPerson);
        var sut = item.LastReservation();
        var reservationCount = item.Reservations.Count;

        var result = sut.CancelReservation("1234");

        Assert.True(result);
        Assert.Equal(reservationCount -1, item.Reservations.Count);
    }
    
    [Fact]
    public void wrongPassWordTest()
    {
        var item = new Item("testName", "testLocation");

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testSøren", "sørentest@email.lol", "søren frichsvej", "12345678");
        
        item.CreateReservation(startDate, endDate, testPerson);
        var sut = item.LastReservation();
        var reservationCount = item.Reservations.Count;
        
        var exeption = Assert.Throws<ArgumentException>(() => sut.CancelReservation("000000000000"));

        Assert.Equal("Forkert kodeord", exeption.Message);
        Assert.Equal(reservationCount, item.Reservations.Count);
    }
}