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

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenInventory();
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
            objectScript.PlayerTrigger(true);

            

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            objectScript = other.GetComponentInParent<InteractableObject>();

            if (_input.playerInteract)
            {
                _input.playerInteract = false;

                objectScript.Interact();
            }
        }
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
