using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public event Action onInventoryChanged;

    public List<Item> inventoryItems;
    
    private Dictionary<ItemType, bool> hasUiInventoryInstance = new Dictionary<ItemType, bool>();

    public Inventory()
    {
        inventoryItems = new List<Item>();
        hasUiInventoryInstance.Add(ItemType.Wood, false);
        hasUiInventoryInstance.Add(ItemType.Stone, false);
        hasUiInventoryInstance.Add(ItemType.Fruit, false);
        hasUiInventoryInstance.Add(ItemType.Money, false); 
        hasUiInventoryInstance.Add(ItemType.Axe, false);

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
        onInventoryChanged?.Invoke();
    }

    public void DestroyUsedToolsInInventory()
    {
        List<Item> removedItems = new List<Item>();

        foreach (Item item in inventoryItems)
        {
            if (item.toolDurability == 0)
            {
                removedItems.Add(item);
            }
        }
        foreach (Item item in removedItems)
        {
            inventoryItems.Remove(item);
        }
      

    }

    public void RemoveItems(List<Item> recipie)
    {
        List<Item> removedItems = new List<Item>();

        foreach (Item recipieItem in recipie)
        {
            foreach (Item item in inventoryItems)
            {
                if (recipieItem.type == item.type)
                {
                    item.amount -= recipieItem.amount;
                    if(item.amount==0)
                    {
                        removedItems.Add(item);
                    }
                }
            }
        }
        foreach (Item item in removedItems)
        {
            inventoryItems.Remove(item);
        }
        onInventoryChanged?.Invoke();

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
        onInventoryChanged?.Invoke();

    }

    public List<Item> GetTools()
    {
        List<Item> result = new List<Item>();
        foreach (Item item in inventoryItems)
        {
            if (item.isTool)
            {
                result.Add(item);
            }
        }

        //result will contain duplicate values if there are exist whitin inventory
        return result;
    }

    public List<Item> GetInventoryItemsList()
    {
        return inventoryItems;
    }
}
