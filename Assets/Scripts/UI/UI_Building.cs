using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Building : MonoBehaviour
{
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;

    private List<Image> chosenImage = new List<Image>();
    private int currentChosenPosition;

    private int timer=500;
    private int timerTick;
    private void Awake()
    {
        RefreshBuildingRecipiesItems();
    }

    public void RefreshBuildingRecipiesItems()
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

        foreach (Recipe recipe in BuildingRecipes.instance.GetAllRecipes())
        {
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
    public Recipe GetRecipeNeeded(Image image)
    {
        if(chosenImage.Count>0)
        {
            for (int i = 0;i < chosenImage.Count;i++)
            {
                if(image== chosenImage[i])
                {
                    return BuildingRecipes.instance.GetAllRecipes()[i];
                }
            }
        }
        return null;
    }
    public void SetImageChoosen(int position)
    {
        if (chosenImage.Count > 0)
        {
            currentChosenPosition = position % chosenImage.Count;
            for (int i = 0; i < chosenImage.Count; i++)
            {
                if (i == currentChosenPosition)
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
        if (chosenImage.Count > 0)
        {
            for (int i = 0; i < chosenImage.Count; i++)
            {
                chosenImage[i].enabled = false;
            }
        }

    }
    public Image ReturnIfChoosenFor5Sec()
    {
        if(chosenImage.Count>0&&GetChosenImageIndex()!=-1)
        {
            Image before = chosenImage[GetChosenImageIndex()];
            if (timerTick < timer)
            {
                timerTick += 1;
                Image after = chosenImage[GetChosenImageIndex()];
                if (before != after)
                {
                    timerTick = 0;
                }
                return null;

            }
            else
            {
                return before;
            }
        }
        
        return null;
    }

    private int GetChosenImageIndex()
    {
        for (int i = 0; i < chosenImage.Count; i++)
        {
            if (chosenImage[i].enabled == true)
            {
                return i;
            }
        }
        return -1;
    }

}
