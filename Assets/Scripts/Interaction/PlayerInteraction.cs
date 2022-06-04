using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;


public class PlayerInteraction : MonoBehaviour
{
    private bool isClicked;
    public event Action<bool> onInventoryClick;

    private StarterAssetsInputs _input;
    private InteractableObject objectScript;
    private CraftingAction crafting;
    private InstaniateObjects building;

    private PlayerInventory inventory;
    private Item interactTool;

    private Animator playerAnimator;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        crafting = GameObject.Find("GameMananger").GetComponent<CraftingAction>();
        building = GameObject.Find("GameMananger").GetComponent<InstaniateObjects>();

        inventory = this.GetComponent<PlayerInventory>();
        playerAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenInventory();
        DebugCrafting();
        BuildItems();
        building.CreateWaitingObject();
    }

    private void BuildItems()
    {
        if (_input.leftClickButton)
        {
            building.Build();
            Debug.Log("Build Here");
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

    private void OpenInventory()
    {
        if (_input.openInventory)
        {
            if (!isClicked)
            {
                isClicked = true;
                onInventoryClick?.Invoke(true);
                _input.openInventory = false;
            }
            else if (isClicked)
            {
                isClicked = false;
                onInventoryClick?.Invoke(false);
                _input.openInventory = false;
            }

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
