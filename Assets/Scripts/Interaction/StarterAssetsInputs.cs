using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class StarterAssetsInputs : MonoBehaviour
{
    public Camera mainCamera;
    [Header("Character Input Values")]

   

    public Vector2 move;
    public Vector2 look;
    public Vector2 mousePosition;
    public bool jump;
    public bool sprint;
    public bool openInventory;
    public bool playerInteract;
    public bool playerCrafting;
    public bool leftClickButton;

    [Header("Movement Settings")]
    public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnOpenInventory(InputValue value)
    {
        OpenInventoryInput(value.isPressed);

    }

    public void OnInteract(InputValue value)
    {
        InteractInput(value.isPressed);

    }
    public void OnCrafting(InputValue value)
    {
        CraftingInput(value.isPressed);

    }
    public void OnLeftClick(InputValue value)
    {
        LeftClickInput(value.isPressed);

    }
    public void OnMousePosition(InputValue value)
    {
        mousePosition = mainCamera.ScreenToWorldPoint(value.Get<Vector2>());
    }



#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }
    public void OpenInventoryInput(bool newOpenInventoryState)
    {
        openInventory = newOpenInventoryState;
    }

    public void InteractInput(bool newInteractState)
    {
        playerInteract = newInteractState;
    }

    private void CraftingInput(bool newCraftingState)
    {
        playerCrafting = newCraftingState;
    }
    private void LeftClickInput(bool isPressed)
    {
        leftClickButton = isPressed;
    }


#if !UNITY_IOS || !UNITY_ANDROID

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

#endif

}
