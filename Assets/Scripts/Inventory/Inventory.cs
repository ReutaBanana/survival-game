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
        var itemsToAdd = new List<Item>();

        if (item.GetIsStackable())
        {
            foreach (Item inventoryItem in inventoryItems)
            {
                if (inventoryItem.type == item.type)
                {
                    if (inventoryItem.amount+ item.amount < item.GetMaxAmount())
                    {
                        inventoryItem.AddAmount(item.amount);
                    }
                    else
                    {
                        inventoryItem.AddAmount(item.GetMaxAmount()- inventoryItem.amount);
                        item.amount = item.GetMaxAmount() - item.amount;
                        itemsToAdd.Add(item);
                    }
                }

            }
            inventoryItems.AddRange(itemsToAdd);
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
