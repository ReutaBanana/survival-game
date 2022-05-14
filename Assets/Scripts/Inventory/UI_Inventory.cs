using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public void SetInventory (Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged1; ;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged1()
    {
        RefreshInventoryItems();
    }

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }
    private void RefreshInventoryItems()
    {
        foreach (Transform transform in itemSlotContainer)
        {
            if (transform == itemSlotTemplate) continue;
            Destroy(transform.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 100f;

        foreach (Item item in inventory.GetInventoryItemsList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y*itemSlotCellSize);
            Image image =itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            x++;

            if(x>3)
            {
                x = 0;
                y++;
            }
        }
    }
}
