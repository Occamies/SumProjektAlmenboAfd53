using SUM.Model;

namespace BookingTest;

public class LastReservationTest
{
    [Fact]
    public void LastReservationSunshineTest()
    {
        var item = new Item("testName", "TestLocation");

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testSøren", "sørentest@email.lol", "søren frichsvej", "12345678");

        var sut = item.CreateReservation(startDate, endDate, testPerson);
        
        Assert.Equal(sut, item.LastReservation());
    }
}