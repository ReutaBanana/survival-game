using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] private GameObject uiMenu;
    [SerializeField] private PlayerInteraction interaction;

    [SerializeField] private GameObject inventoryContiner;
    [SerializeField] private GameObject craftingContiner;
    [SerializeField] private GameObject buildingContiner;

    private UI_Inventory uiInventoryScript;

    private bool inventoryContinerOpen;
    private bool craftingContinerOpen;
    private bool buildingContinerOpen;

    private int currentPositionInContiner = 1;

    // Start is called before the first frame update
    void Start()
    {
        interaction.onMenuClick += ShowMenu;
        interaction.onScrollMenu += ScrollMenu;
        uiInventoryScript = inventoryContiner.GetComponent<UI_Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    private void ScrollMenu(ScrollLocation location)
    {  
       if(location==ScrollLocation.Up)
        {
            if(inventoryContinerOpen)
            {
                if (currentPositionInContiner < uiInventoryScript.GetInventoryLength())
                {
                    currentPositionInContiner++;
                }
                else
                {
                    currentPositionInContiner = 0;

                }
                UpdateSelectedPosition(uiInventoryScript.GetChosenImage());

            }
        }

    }
    private void UpdateSelectedPosition(List<Image> chosenImageList)
    {
        
        for (int i = 0; i < chosenImageList.Count; i++)
        {
            if (i==currentPositionInContiner)
            {
                chosenImageList[i].GetComponent<Image>().enabled = true;
            }
            chosenImageList[i].GetComponent<Image>().enabled = false;
        }
    }
    private void SetFirstChosen()
    {
        if (inventoryContinerOpen)
        {
            List<Image> chosenImages = uiInventoryScript.GetChosenImage();
            chosenImages[1].enabled = true;
        }
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
        SetFirstChosen();

    }
}
