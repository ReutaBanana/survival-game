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
}
