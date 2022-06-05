using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Crafting : MonoBehaviour
{
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;

    private List<Image> chosenImage = new List<Image>();
    private void Awake()
    {
        Debug.Log(CraftingRecipes.instance.GetAllRecipes().Count);
        RefreshCraftingRecipiesItems();
    }

    public void RefreshCraftingRecipiesItems()
    {
        foreach (Transform transform in itemSlotContainer)
        {
            if (transform == itemSlotTemplate) continue;
            Destroy(transform.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 115f;

        foreach (Recipe recipe in CraftingRecipes.instance.GetAllRecipes())
        {
            Debug.Log(recipe.GetOutputItem().GetType());
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            chosenImage.Add(itemSlotRectTransform.Find("Choosen").GetComponent<Image>());
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = recipe.GetOutputItem().GetSprite();

                x++;

                if (x >= 2)
                {
                    x = 0;
                    y--;
                }          
            
        }
    }
    public List<Image> GetChosenImage()
    {
        return chosenImage;
    }
}
