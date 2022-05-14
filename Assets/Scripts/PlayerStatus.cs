using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int _maxHunger;
    [SerializeField] private int _decreasePassiveAmount = 3;
    [SerializeField] private float _decreaseTimer = 3;

    [SerializeField] UI_Inventory uiInventory;

    private int currentHunger;
    private float updateTimer;
    private Inventory playerInventory;

    //add here string for different statuses
    public event Action<int> onStatusChange;

    // Start is called before the first frame update
    void Start()
    {
        //add from save file
        currentHunger = _maxHunger;
    }
    private void Awake()
    {
        playerInventory = new Inventory();
        uiInventory.SetInventory(playerInventory);

        ItemWorld.SpawnItemWorld(new Vector3(0, 0, 0), new Item { type = Item.ItemType.Wood, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 0, 0), new Item { type = Item.ItemType.Fruit, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 0, 0), new Item { type = Item.ItemType.Money, amount = 1 });
        ItemWorld.SpawnItemWorld(new Vector3(0, 0, 0), new Item { type = Item.ItemType.Stone, amount = 1 });
    }
    // Update is called once per frame
    void Update()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer > _decreaseTimer)
        {
            DecreaseHunger(_decreasePassiveAmount);
            onStatusChange?.Invoke(currentHunger);
            updateTimer = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponentInParent<ItemWorld>();
        if(itemWorld!=null)
        {
            playerInventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }        
    }
    //change names and add string/enum property for changing different statuses
    public int GetCurrentHunger()
    {
        return currentHunger;
    }
    public void DecreaseHunger(int amount)
    {
        if (currentHunger - amount > 0)
        {
            currentHunger -= amount;
        }
        else
        {
            currentHunger = 0;
        }
        onStatusChange?.Invoke(currentHunger);
    }
    public void IncreaserHunger(int amount)
    {
        if (currentHunger + amount < _maxHunger)
        {
            currentHunger += amount;
        }
        else
        {
            currentHunger = _maxHunger;
        }
        onStatusChange?.Invoke(currentHunger);
    }
}
