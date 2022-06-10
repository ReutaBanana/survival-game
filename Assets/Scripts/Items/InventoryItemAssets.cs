using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemAssets : MonoBehaviour
{
   public static InventoryItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    [Header("Items")]
    public Sprite woodSprite;
    public Sprite fruitSprite;
    public Sprite stoneSprite;
    public Sprite moneySprite;
    
    [Header("Crafting")]
    public Sprite axeSprite;
    public Sprite pickaxeSprite;

    [Header("Building")]
    public Sprite campfireSprite;


}
