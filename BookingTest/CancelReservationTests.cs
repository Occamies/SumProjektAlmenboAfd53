using SUM.Model;

namespace BookingTest;

public class CancelReservationTests
{
    [Fact]
    public void SunshineTest()
    {
        var item = new Item("testName", "testLocation");

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var testPerson = new Person("testSøren", "sørentest@email.lol", "søren frichsvej", "12345678");

        item.CreateReservation(startDate, endDate, testPerson);
        var sut = item.LastReservation();

        var result = sut.CancelReservation("1234");

        Assert.True(result);
    }
}