using System.Collections.Generic;

namespace API.Models;

public class Room : Item
{
    public List<Item> Inventory { get; set; }

    public Room(string name, string location) : base(name, location)
    {
        Inventory = [];
    }

    public void AddToInventory(Item item)
    {
        Inventory.Add(item);
    }

    public void RemoveFromInventory(Item item)
    {
        Inventory.Remove(item);
    }
}