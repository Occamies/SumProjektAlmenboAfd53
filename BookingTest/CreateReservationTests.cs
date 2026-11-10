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
    public void NullPersonTest()
    //Test for om 
    {
        var sut = new Item("testName", "testLocation");
    }
}