using System;
using SUM.Model;
using Xunit;

namespace BookingTest;

public class CreateReservationTests
{
    //SUT = System Under Test
    //Test for det perfekte scenarie hvor reservationen går igennem
    [Fact]
    public void CreateReservationSunshineTest()
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testSøren", "sørentest@email.lol", "søren frichsvej", "12345678");
        var reservationCount = sut.Reservations.Count;
        
        sut.CreateReservation(startDate, endDate, testPerson);
        var newReservationCount = sut.Reservations.Count;
        
        Assert.Equal(sut.LastReservation().StartDate, startDate );
        Assert.Equal(reservationCount+1, newReservationCount);
    }
    
    //Test for om den giver fejl når startDate er før slutdato
    [Fact]
    public void EndDateBeforeStartDateTest()
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(-1);
        var testPerson = new Person("testSøren", "sørentest@email.lol", "søren frichsvej", "12345678");
        var reservationCount = sut.Reservations.Count;
        
        var exception = Assert.Throws<ArgumentException>(() =>
            sut.CreateReservation(startDate, endDate, testPerson));
        
        Assert.Equal("EndDate skal være efter StartDate", exception.Message);
        Assert.Equal(reservationCount, sut.Reservations.Count);
        
    }
    
    //Test for om den giver fejl når der Startdato er før nuværende dato
    [Fact]
    public void StartDateBeforeTodayTest()
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now.AddDays(-1);
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testSøren", "sørentest@email.lol", "søren frichsvej", "12345678");
        var reservationCount = sut.Reservations.Count;
        
        var exception = Assert.Throws<ArgumentException>(() =>
            sut.CreateReservation(startDate, endDate, testPerson));
        
        Assert.Equal("StartDate skal tidligst idag", exception.Message);
        Assert.Equal(reservationCount, sut.Reservations.Count);
    }
}