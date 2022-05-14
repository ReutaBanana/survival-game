using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory 
{
    public event Action OnItemListChanged;

    private List<Item> inventoryItems;

    public Inventory()
    {
        inventoryItems = new List<Item>();
        Debug.Log("My inventory is alive!");
    }
    public void AddItem(Item item)
    {
        inventoryItems.Add(item);
        OnItemListChanged?.Invoke();
    }
    public List<Item> GetInventoryItemsList()
    {
        return inventoryItems;
    }
}
