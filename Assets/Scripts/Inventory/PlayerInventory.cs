using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] UI_Inventory uiInventory;
    private Inventory playerInventory;

    private void Awake()
    {
        playerInventory = new Inventory();
        uiInventory.SetInventory(playerInventory);
    }
    private void Update()
    {
        playerInventory.DestroyUsedToolsInInventory();
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponentInParent<ItemWorld>();
        if (itemWorld != null)
        {
            playerInventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    public Inventory GetInventory()
    {
        return playerInventory;
    }

}
