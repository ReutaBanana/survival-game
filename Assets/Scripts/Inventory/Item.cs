using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Wood,
        Fruit,
        Money,
        Stone
    }

    public ItemType type;
    public int amount;
    public bool hasInventoryInstance = false;

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
            default:
                return false;
        }
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
                return ItemAssets.Instance.woodSprite;
            case ItemType.Fruit:
                return ItemAssets.Instance.fruitSprite;
            case ItemType.Money:
                return ItemAssets.Instance.moneySprite;
            case ItemType.Stone:
                return ItemAssets.Instance.stoneSprite;
        }
    }
    public Transform GetItemPrefab()
    {
        switch (type)
        {
            default:
            case ItemType.Wood:
                return ItemAssets.Instance.woodPrefab;
            case ItemType.Fruit:
                return ItemAssets.Instance.fruitPrefab;
            case ItemType.Money:
                return ItemAssets.Instance.moneyPrefab;
            case ItemType.Stone:
                return ItemAssets.Instance.stonePrefab;

        }
    }
    
    public void AddAmount(int amount)
    {
        this.amount += amount;
    }
    public void SetInventoryInstance()
    {
        hasInventoryInstance = true;
    }
}
