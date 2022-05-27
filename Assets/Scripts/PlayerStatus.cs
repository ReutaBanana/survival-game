using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int _maxHunger;
    [SerializeField] private int _decreasePassiveAmount = 3;
    [SerializeField] private float _decreaseTimer = 3;


    private int currentHunger;
    private float updateTimer;

    //add here string for different statuses
    public event Action<int> onStatusChange;

    // Start is called before the first frame update
    void Start()
    {
        //add from save file
        currentHunger = _maxHunger;
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
