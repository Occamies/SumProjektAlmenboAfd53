using System;
using API.Models;
using Xunit;

namespace BookingTest;

public class CreateReservationTests
{
    //SUT = System Under Test.
    //Test for det perfekte scenarie hvor reservationen går igennem
    [Fact]
    public void CreateReservationSunshineTest()
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testperson", "persontest@email.lol", "testvej", "12345678");
        var reservationCount = sut.Reservations.Count;
        
        sut.CreateReservation(startDate, endDate, testPerson);
        var newReservationCount = sut.Reservations.Count;
        
        Assert.Equal(sut.LastReservation().StartDate, startDate );
        Assert.Equal(reservationCount+1, newReservationCount);
    }
    
    //Test for om den giver fejl når slutDate er før startdato
    [Fact]
    public void EndDateBeforeStartDateTest()
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(-1);
        var testPerson = new Person("testperson", "persontest@email.lol", "testvej", "12345678");
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
        var testPerson = new Person("testperson", "persontest@email.lol", "testvej", "12345678");
        var reservationCount = sut.Reservations.Count;
        
        var exception = Assert.Throws<ArgumentException>(() =>
            sut.CreateReservation(startDate, endDate, testPerson));
        
        Assert.Equal("StartDate skal tidligst være idag", exception.Message);
        Assert.Equal(reservationCount, sut.Reservations.Count);
    }
    
    //Test for at sikre der ikke kan opstå dobbelt booking
    [Fact]
    public void DobbeltBookingQuestionMarkQuestionMarkQuestionMarkQuestionMarkTest()
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testperson", "persontest@email.lol", "testvej", "12345678");
        var testPerson2 = new Person("Lasse", "LasseHarEnMail@hotmail.dk", "Hjemme hos Lasse Gade", "45069420");
        
        sut.CreateReservation(startDate, endDate, testPerson);
        
        var exception = Assert.Throws<InvalidOperationException>(() =>
            sut.CreateReservation(startDate, endDate, testPerson2));
        
        Assert.Equal("Reservationen er ikke tilgængelig", exception.Message);
    }
    
    //Test for at sikre at person ikke er null
    [Fact]
    public void CreateReservationNullPersonTest()
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        
        var exception = Assert.Throws<ArgumentException>(() =>
            sut.CreateReservation(startDate, endDate, null));
        
        Assert.Equal("Bruger mangler", exception.Message);
    }
    
    //Test for at sikre at startdate ikke er null
    [Fact]
    public void CreateReservationNullStartDateTest()
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testperson", "persontest@email.lol", "testvej", "12345678");

        
        var exception = Assert.Throws<ArgumentException>(() =>
            sut.CreateReservation(default, endDate, testPerson));
        
        Assert.Equal("Startdato skal tilføjes", exception.Message);
    }
    
    //Test for at sikre at enddate ikke er null
    [Fact]
    public void CreateReservationNullEndDateTest()
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now;
        var testPerson = new Person("testperson", "persontest@email.lol", "testvej", "12345678");

        
        var exception = Assert.Throws<ArgumentException>(() =>
            sut.CreateReservation(startDate, default, testPerson));
        
        Assert.Equal("Slutdato skal tilføjes", exception.Message);
    }
}