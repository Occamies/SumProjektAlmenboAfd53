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
        item.Update(item.Name, this.Location);
    }

    public void RemoveFromInventory(Item item)
    {
        if (!Inventory.Contains(item))
        {
            throw new ArgumentException("Object ikke i inventory");
        }
        Inventory.Remove(item);
        
    }
}