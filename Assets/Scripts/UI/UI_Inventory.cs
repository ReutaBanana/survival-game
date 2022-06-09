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
    private List<Image> chosenImage = new List<Image>();

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
        chosenImage.Clear();
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
            chosenImage.Add(itemSlotRectTransform.Find("Choosen").GetComponent<Image>());

            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            x++;

            if (x >= 2)
            {
                x = 0;
                y--;
            }

            if (item.GetIsStackable() && item.amount > 1)
            {
                Transform uiStackable = itemSlotRectTransform.Find("objectCount");
                uiStackable.GetComponent<Image>().enabled = true;
                uiStackable.Find("objectCountTxt").GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
                uiStackable.Find("objectCountTxt").GetComponent<TextMeshProUGUI>().enabled = true;

            }
        }
        if (chosenImage.Count > 0)  
        { chosenImage[currentChosenPosition].enabled = true; }
    }
    public List<Image> GetChosenImage()
    {
        return chosenImage;
    }
    public void SetImageChoosen(int position)
    {
        if (chosenImage.Count>0)
        {
            currentChosenPosition = position % chosenImage.Count;
            for (int i = 0; i < chosenImage.Count; i++)
            {
                if(i== currentChosenPosition)
                {
                    chosenImage[i].enabled = true;
                }
                else
                {
                    chosenImage[i].enabled = false;
                }
            }
        }
    }

    internal void clearChoosen()
    {
        if(chosenImage.Count>0)
        {
            for (int i = 0; i < chosenImage.Count; i++)
            { 
                chosenImage[i].enabled = false;
            }
        }
       
    }
}
