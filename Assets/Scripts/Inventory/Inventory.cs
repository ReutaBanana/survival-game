using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public event Action OnItemListChanged;

    private List<Item> inventoryItems;
    private Dictionary<Item.ItemType, bool> hasInventoryInstance = new Dictionary<Item.ItemType, bool>();

    public Inventory()
    {
        inventoryItems = new List<Item>();
        hasInventoryInstance.Add(Item.ItemType.Wood, false);
        hasInventoryInstance.Add(Item.ItemType.Stone, false);
        hasInventoryInstance.Add(Item.ItemType.Fruit, false);
        hasInventoryInstance.Add(Item.ItemType.Money, false);
    }
    public void AddItem(Item item)
    {
        if (hasInventoryInstance[item.type]==false)
        {
            inventoryItems.Add(item);
            item.SetInventoryInstance();
            hasInventoryInstance[item.type] = true;
        }
        else
        {
            CheckIfStackable(item);
        }
       OnItemListChanged?.Invoke();
    }

    private void CheckIfStackable(Item item)
    {
        if (item.GetIsStackable())
        {
            foreach (Item inventoryItem in inventoryItems)
            {
                if (inventoryItem.type == item.type)
                {
                    if (item.amount < item.GetMaxAmount())
                    {
                        inventoryItem.AddAmount(item.amount);
                    }
                }
                else
                {
                    inventoryItems.Add(item);
                }
            }
        }
        else
        {
            inventoryItems.Add(item);
        }

    }

    public List<Item> GetInventoryItemsList()
    {
        return inventoryItems;
    }
}
