using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
   public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite woodSprite;
    public Sprite fruitSprite;
    public Sprite stoneSprite;
    public Sprite moneySprite;


    public Transform woodPrefab;
    public Transform fruitPrefab;
    public Transform stonePrefab;
    public Transform moneyPrefab;
}
