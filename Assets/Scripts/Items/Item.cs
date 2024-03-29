using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    //base items
    Wood,
    Fruit,
    Money,
    Stone,
    //crafted items
    Axe,
    Campfire,
    Pickaxe
}
public class Item 
{
    public ItemType type;
    public int amount;
    public bool isTool;
    public int toolDurability;


    public Item(ItemType type, int amount)
    {
        this.type = type;
        this.amount = amount;
        SetIsTool();
    }
    
    public bool GetIsStackable()
    {
        switch (type)
        {
            case ItemType.Wood:
                return true;
            case ItemType.Fruit:
                return true;
            case ItemType.Money:
                return true;
            case ItemType.Stone:
                return true;
            case ItemType.Axe:
                return false;
            default:
                return false;
        }
    }
    public void SetIsTool()
    {
        switch (type)
        {
            case ItemType.Axe:
                isTool = true;
                toolDurability = 3;
                break;
            default:
                isTool = false;
                toolDurability = -1;
                break;
        }
    }

    public void DecreseDurability()
    {
        toolDurability--;
    }

    public bool IsAbliageForHit()
    {
        if (isTool&&toolDurability>0)
        {
            return true;
        }
        else if(isTool&&toolDurability==0)
        {
            return false;
        }

        return true;
        

    }
    public int GetMaxAmount()
    {
        switch (type)
        {
            case ItemType.Wood:
                return 30;
            case ItemType.Fruit:
                return 15;
            case ItemType.Money:
                return 10;
            case ItemType.Stone:
                return 30 ;
            default:
                return 0;
        }
    }
    public Sprite GetSprite()
    {
        switch (type)
        {
            default:
            case ItemType.Wood:
                return InventoryItemAssets.Instance.woodSprite;
            case ItemType.Fruit:
                return InventoryItemAssets.Instance.fruitSprite;
            case ItemType.Money:
                return InventoryItemAssets.Instance.moneySprite;
            case ItemType.Stone:
                return InventoryItemAssets.Instance.stoneSprite;
            case ItemType.Axe:
                return InventoryItemAssets.Instance.axeSprite;
            case ItemType.Campfire:
                return InventoryItemAssets.Instance.campfireSprite;
            case ItemType.Pickaxe:
                return InventoryItemAssets.Instance.pickaxeSprite;


        }
    }
    
    public void AddAmount(int amount)
    {
        this.amount += amount;
    }
   
}
