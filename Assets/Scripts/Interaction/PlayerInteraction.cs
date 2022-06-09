using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public enum ScrollLocation
{
    Up,
    Down,
    none
}
public class PlayerInteraction : MonoBehaviour
{
    private bool isClicked;
    public event Action<bool,int> onMenuClick;
    public event Action<ScrollLocation> onScrollMenu;


    private StarterAssetsInputs _input;
    private InteractableObject objectScript;
    private CraftingAction crafting;
    private InstaniateObjects building;
    private UI_Menu uiMenuScript;

    private PlayerInventory inventory;
    private Item interactTool;

    private bool canBuild;
    private BuildingRecipeType currentType;

    private Animator playerAnimator;
    private void Start()
    {
        uiMenuScript.onBuildingImageStay += UiMenuScript_onBuildingImageStay;
    }

    private void UiMenuScript_onBuildingImageStay(Recipe obj)
    {
        currentType = obj.GetBuildingRecipeType();
        building.CreateWaitingObject(currentType);
        canBuild = true;

    }

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        crafting = GameObject.Find("GameMananger").GetComponent<CraftingAction>();
        building = GameObject.Find("GameMananger").GetComponent<InstaniateObjects>();
        uiMenuScript = GameObject.Find("Canvas").GetComponent<UI_Menu>();
        inventory = this.GetComponent<PlayerInventory>();
        playerAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMenu();
        DebugCrafting();
        BuildItems();
        CheckScrollPosition();
    }

    private void BuildItems()
    {

        if (_input.leftClickButton)
        {
            if(canBuild)
            {
                building.Build(currentType);
                canBuild = false;
            }
            _input.leftClickButton = false;
        }
    }

    private void DebugCrafting()
    {
        if (_input.playerCrafting)
        {
            crafting.Craft(CraftingRecepieType.AxeRecipe);
            _input.playerCrafting = false;
        }
    }
    private void CheckScrollPosition()
    {
        if(_input.chooseInMenu.y>0)
        {
            _input.chooseInMenu.y = 0;
            onScrollMenu?.Invoke(ScrollLocation.Up);
        }
        else if(_input.chooseInMenu.y < 0)
        {
            _input.chooseInMenu.y = 0;
            onScrollMenu?.Invoke(ScrollLocation.Down);
        }
        else if (_input.chooseInMenu.y == 0)
        {
            onScrollMenu?.Invoke(ScrollLocation.none);
        }
    }
    private void HandleMenu()
    {
        if (_input.openUIMenu)
        {
            Debug.Log("Cliked");
            if (!isClicked)
            {
                isClicked = true;
                onMenuClick?.Invoke(true,0);
                _input.openUIMenu = false;
            }
            else if (isClicked)
            {
                isClicked = false;
                onMenuClick?.Invoke(false,0);
                _input.openUIMenu = false;
            }

        }
        if(_input.inventoryUIMenu)
        {
            onMenuClick?.Invoke(true, 1);
            _input.inventoryUIMenu = false;
        }
        if(_input.craftingUIMenu)
        {
            onMenuClick?.Invoke(true, 2);
            _input.craftingUIMenu = false;
        }
        if(_input.buildingUIMenu)
        {
            onMenuClick?.Invoke(true, 3);
            _input.buildingUIMenu = false;
        }
        if(_input.rightClick)
        {
            _input.rightClick = false;
            onMenuClick?.Invoke(true, -1);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            objectScript = other.GetComponentInParent<InteractableObject>();
            interactTool = objectScript.GetToolIfAvailable(inventory);

            if (WantsToInteract())
            {
                objectScript.PlayerTrigger(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            objectScript = other.GetComponentInParent<InteractableObject>();
            interactTool = objectScript.GetToolIfAvailable(inventory);

            if ( _input.playerInteract)
            {
                _input.playerInteract = false;

                if (WantsToInteract())
                {
                    objectScript.Interact();
                    playerAnimator.SetTrigger("ChopTree");
                    if (interactTool != null)
                        interactTool.DecreseDurability();
                }

            }
        }
    }

    private bool WantsToInteract()
    {
        return (interactTool != null && interactTool.IsAbliageForHit()) || !objectScript.GetInteractDependency();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            InteractableObject objectScript = other.GetComponentInParent<InteractableObject>();
            objectScript.PlayerTrigger(false);
        }
    }

}
