using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform tranform = Instantiate(item.GetItemPrefab(), position, Quaternion.identity);
        ItemWorld itemWorld = tranform.GetComponent<ItemWorld>();

        return itemWorld;
    }

    public Item.ItemType itemType;
    public int amount;

    public Item GetItem()
    {
        return new Item{ type = itemType, amount = amount };
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }


}
