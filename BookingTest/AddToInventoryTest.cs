using SUM.Model;
using Xunit;

namespace BookingTest;

public class AddToInventoryTest
{
    [Fact]
    public void AddToInventorySunshineTest()
    {
        var item = new Item("testName", "testLocataion");
        var sut = new Room("testRoom", "testRoomLocation");
        
        sut.AddToInventory(item);
        
        Assert.Equal(sut.Inventory[sut.Inventory.Count-1], item);
    }
}