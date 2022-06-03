using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public Item.ItemType itemType;
    public int amount;

    public Item GetItem()
    {
        return new Item(itemType,amount);
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }


}
