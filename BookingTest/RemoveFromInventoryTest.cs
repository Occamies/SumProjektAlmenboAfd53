using SUM.Model;

namespace BookingTest;
using Xunit;

public class RemoveFromInventoryTest
{
    // Tester om Items fjernes rigtigt Test
    [Fact]
    public void RemoveFromInventorySunshineTest()
    {
        var item = new Item("testName", "testLocataion");
        var item2 = new Item("testName", "testLocataion");
        var item3 = new Item("testName", "testLocataion");
        
        var sut = new Room("testRoom", "testRoomLocation");
        
        sut.AddToInventory(item);
        sut.AddToInventory(item2);
        sut.AddToInventory(item3);
        
        sut.RemoveFromInventory(item2);
        
        Assert.DoesNotContain(item2, sut.Inventory);
        Assert.Contains(item,  sut.Inventory);
        Assert.Contains(item3, sut.Inventory);
    }

    // Tjekker om Item findes
    [Fact]
    public void ItemNotInInventoryTest()
    {
        var item = new Item("testName", "testLocataion");
        var item2 = new Item("testName", "testLocataion");
        var item3 = new Item("testName", "testLocataion");
        
        var sut = new Room("testRoom", "testRoomLocation");
        
        sut.AddToInventory(item);
        sut.AddToInventory(item3);
        //TODO tjek om exeption er rigtig
        var exeption = Assert.Throws<ArgumentException>(() => sut.RemoveFromInventory(item2));
        
        Assert.Contains(exeption.Message, @"Object ikke i inventory");
    }
}