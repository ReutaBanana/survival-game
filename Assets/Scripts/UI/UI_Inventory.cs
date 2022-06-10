using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;

    private int inventoryLength;
    private int currentChosenPosition;
    public void SetInventory (Inventory inventory)
    {
        this.inventory = inventory;
    }
    private void Start()
    {
        inventory.onInventoryChanged += RefreshInventoryItems;
    }
    private void Update()
    {
        inventoryLength = inventory.inventoryItems.Count;

    }

  public int GetInventoryLength()
    {
        return inventoryLength;
    }

    public void RefreshInventoryItems()
    {
        foreach (Transform transform in itemSlotContainer)
        {
            if (transform == itemSlotTemplate) continue;
            Destroy(transform.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 115f;

        foreach (Item item in inventory.GetInventoryItemsList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();

            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            x++;

            if (x >= 25)
            {
              
            }

            if (item.GetIsStackable() && item.amount > 1)
            {
                itemSlotRectTransform.Find("objectCountTxt").GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
                itemSlotRectTransform.Find("objectCountTxt").GetComponent<TextMeshProUGUI>().enabled = true;

            }
        }
    }
}
