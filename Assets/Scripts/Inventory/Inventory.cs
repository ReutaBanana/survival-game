using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<Item> inventoryItems;

    public Inventory()
    {
        inventoryItems = new List<Item>();

        AddItem (new Item { type = Item.ItemType.Fruit, amount = 1 } );
        AddItem(new Item { type = Item.ItemType.Wood, amount = 4 });
        AddItem(new Item { type = Item.ItemType.Stone, amount = 3 });
        Debug.Log("My inventory is alive!");
    }
    public void AddItem(Item item)
    {
        inventoryItems.Add(item);
    }
    public List<Item> GetInventoryItemsList()
    {
        return inventoryItems;
    }
}
