using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public event Action OnItemListChanged;

    private List<Item> inventoryItems;
    
    private Dictionary<Item.ItemType, bool> hasUiInventoryInstance = new Dictionary<Item.ItemType, bool>();

    public Inventory()
    {
        inventoryItems = new List<Item>();
        hasUiInventoryInstance.Add(Item.ItemType.Wood, false);
        hasUiInventoryInstance.Add(Item.ItemType.Stone, false);
        hasUiInventoryInstance.Add(Item.ItemType.Fruit, false);
        hasUiInventoryInstance.Add(Item.ItemType.Money, false); 
        hasUiInventoryInstance.Add(Item.ItemType.Axe, false);

    }
    public void AddItem(Item item)
    {
        if (hasUiInventoryInstance[item.type]==false)
        {
            inventoryItems.Add(item);
            hasUiInventoryInstance[item.type] = true;
        }
        else
        {
            CheckIfStackable(item);
        }
       OnItemListChanged?.Invoke();
    }

    public void RemoveItem(List<Item> recipie)
    {
        foreach (Item recipieItem in recipie)
        {
            foreach (Item item in inventoryItems)
            {
                if (recipieItem.type == item.type)
                {
                    item.amount -= recipieItem.amount;
                }
            }
        }
        OnItemListChanged?.Invoke();

    }

    public bool CheckIfExsist(Item recipieItem)
    {
        foreach (Item item in inventoryItems)
        {
            if( recipieItem.type == item.type && recipieItem.amount<= item.amount)
            {
                return true;
            }
        }
        return false;
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
