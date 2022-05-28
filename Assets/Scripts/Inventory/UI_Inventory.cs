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

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private PlayerInteraction interaction;
    public void SetInventory (Inventory inventory)
    {
        this.inventory = inventory;
        interaction.onInventoryClick += ShowInventory;
    }

    private void ShowInventory(bool isOpen)
    {
        RefreshInventoryItems();
        switch (isOpen)
        {            
            case true:
                inventoryUI.SetActive(true);
                break;
            default:
            case false:
                inventoryUI.SetActive(false);
                break;      
        }
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

            if (item.GetIsStackable() && item.amount > 1)
            {
                Transform uiStackable = itemSlotRectTransform.Find("objectCount");
                uiStackable.GetComponent<Image>().enabled= true;
                uiStackable.Find("objectCountTxt").GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
                uiStackable.Find("objectCountTxt").GetComponent<TextMeshProUGUI>().enabled = true;

            }
        }
    }
}
