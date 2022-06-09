using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] private GameObject uiMenu;
    [SerializeField] private PlayerInteraction interaction;

    [SerializeField] private GameObject inventoryContiner;
    [SerializeField] private GameObject craftingContiner;
    [SerializeField] private GameObject buildingContiner;

    private UI_Inventory uiInventoryScript;
    private UI_Crafting uiCraftingScript;
    private UI_Building uiBuildingScript;


    private bool inventoryContinerOpen;
    private bool craftingContinerOpen;
    private bool buildingContinerOpen;

    private int currentPositionInContiner = 1;

    public event Action<Recipe> onBuildingImageStay;
    // Start is called before the first frame update
    void Start()
    {
        interaction.onMenuClick += ShowMenu;
        interaction.onScrollMenu += ScrollMenu;
        uiInventoryScript = inventoryContiner.GetComponent<UI_Inventory>();
        uiCraftingScript = craftingContiner.GetComponent<UI_Crafting>();
        uiBuildingScript = buildingContiner.GetComponent<UI_Building>();
    }
    private void Update()
    {
        if(uiBuildingScript.ReturnIfChoosenFor5Sec()!=null)
        {
            onBuildingImageStay?.Invoke(uiBuildingScript.GetRecipeNeeded(uiBuildingScript.ReturnIfChoosenFor5Sec()));
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
        if (inventoryContinerOpen)
            uiInventoryScript.SetImageChoosen(currentPositionInContiner);
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
                    uiInventoryScript.RefreshInventoryItems();
                    break;
                default:
                case false:
                    uiMenu.SetActive(false);
                    break;
            }
        }
        if (menuSlot == 1)
        {
            inventoryContiner.SetActive(true);
            craftingContiner.SetActive(false);
            buildingContiner.SetActive(false);
            inventoryContinerOpen = true;
            craftingContinerOpen = false;
            buildingContinerOpen = false;
            uiInventoryScript.RefreshInventoryItems();
        }
               
        if (menuSlot == 2)
        {
            inventoryContiner.SetActive(false);
            craftingContiner.SetActive(true);
            buildingContiner.SetActive(false);
            inventoryContinerOpen = false;
            craftingContinerOpen = true;
            buildingContinerOpen = false;
        }
             
        if (menuSlot == 3)
        {
            inventoryContiner.SetActive(false);
            craftingContiner.SetActive(false);
            buildingContiner.SetActive(true);
            inventoryContinerOpen = false;
            craftingContinerOpen = false;
            buildingContinerOpen = true;
        }
        if(menuSlot == -1)
        {
            if (inventoryContinerOpen)
                uiInventoryScript.clearChoosen();
        }
    }
}
