using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] private GameObject uiMenu;
    [SerializeField] private PlayerInteraction interaction;

    [SerializeField] private GameObject craftingContiner;
    [SerializeField] private GameObject buildingContiner;

    private UI_Crafting uiCraftingScript;
    private UI_Building uiBuildingScript;

    private bool craftingContinerOpen;
    private bool buildingContinerOpen;

    private int currentPositionInContiner = 1;

    public event Action<Recipe,String> onImageStayEvent;
    // Start is called before the first frame update
    void Start()
    {
        interaction.onMenuClick += ShowMenu;
        interaction.onScrollMenu += ScrollMenu;
        uiCraftingScript = craftingContiner.GetComponent<UI_Crafting>();
        uiBuildingScript = buildingContiner.GetComponent<UI_Building>();
    }
    private void Update()
    {
        if(uiBuildingScript.ReturnIfChoosenFor5Sec()!=null)
        {
            onImageStayEvent?.Invoke(uiBuildingScript.GetRecipeNeeded(uiBuildingScript.ReturnIfChoosenFor5Sec()),"Build");
        }
        if (uiCraftingScript.ReturnIfChoosenFor5Sec() != null)
        {
            onImageStayEvent?.Invoke(uiCraftingScript.GetRecipeNeeded(uiCraftingScript.ReturnIfChoosenFor5Sec()),"Craft");
        }
    }

    private void ScrollMenu(ScrollLocation location)
    {  
       if(location==ScrollLocation.Up)
        {
            currentPositionInContiner++;
            SetChosenImageToAllContainers();
        }
        if (location == ScrollLocation.Down)
        {
            currentPositionInContiner--;
            SetChosenImageToAllContainers();
        }
    }

    private void SetChosenImageToAllContainers()
    {
        if (craftingContinerOpen)
            uiCraftingScript.SetImageChoosen(currentPositionInContiner);
        if (buildingContinerOpen)
            uiBuildingScript.SetImageChoosen(currentPositionInContiner);
    }

    private void ShowMenu(bool isOpen,int menuSlot)
    {
        if(menuSlot==0)
        {
            switch (isOpen)
            {
                case true:
                    uiMenu.SetActive(true);
                    break;
                default:
                case false:
                    uiMenu.SetActive(false);
                    uiBuildingScript.clearChoosen();
                    uiCraftingScript.clearChoosen();
                    break;
            }
        }    
        if (menuSlot == 2)
        {
            craftingContiner.SetActive(true);
            buildingContiner.SetActive(false);
            craftingContinerOpen = true;
            buildingContinerOpen = false;
        }
             
        if (menuSlot == 3)
        {
            craftingContiner.SetActive(false);
            buildingContiner.SetActive(true);
            craftingContinerOpen = false;
            buildingContinerOpen = true;
        }
        if(menuSlot == -1)
        {
            uiBuildingScript.clearChoosen();
            uiCraftingScript.clearChoosen();
        }
    }
}
