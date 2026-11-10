using SUM.Model;

namespace BookingTest;

public class CreateReservationTests
{
    [Fact]
    public void SunshineTest()
    //Test for det perfekte scenarie hvor reservationen går igennem
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testSøren", "sørentest@email.lol", "søren frichsvej", "12345678");
        var reservationCount = sut.Reservations.Count+1;
        
        sut.CreateReservation(startDate, endDate, testPerson);
        var newReservationCount = sut.Reservations.Count;
        
        Assert.Equal(sut.LastReservation().StartDate, startDate );
        Assert.Equal(reservationCount, newReservationCount);
    }
    
    [Fact]
    public void EndDateBeforeStartDateTest()
    //Test for om den giver fejl når startDate er før slutdato
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(-1);
        var testPerson = new Person("testSøren", "sørentest@email.lol", "søren frichsvej", "12345678");
        var reservationCount = sut.Reservations.Count;
        
        sut.CreateReservation(startDate, endDate, testPerson);
        var newReservationCount = sut.Reservations.Count;
        
        Assert.Throws<ArgumentException>(()=>
            sut.CreateReservation(startDate, endDate, testPerson));
        Assert.Equal(reservationCount, newReservationCount);
        
    }
    
    [Fact]
    public void StartDateBeforeTodayTest()
        //Test for om den giver fejl når der Startdato er før nuværende dato
    {
        var sut = new Item("testName", "testLocation");
        
        var startDate = DateTime.Now.AddDays(-1);
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testSøren", "sørentest@email.lol", "søren frichsvej", "12345678");
        var reservationCount = sut.Reservations.Count;
        
        sut.CreateReservation(startDate, endDate, testPerson);
        var newReservationCount = sut.Reservations.Count;
        
        //TODO fix assert
        Assert.Throws<ArgumentException>(()=>
            sut.CreateReservation(startDate, endDate, testPerson));
        
        
        
        Assert.Equal(reservationCount, newReservationCount);
        
    }
}