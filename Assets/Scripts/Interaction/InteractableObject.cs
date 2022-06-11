using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum interactionType
{
    ChopTree,
    StoneCollect,
    StoneDig,
    LightCamfire
}


public class InteractableObject : MonoBehaviour
{
    [SerializeField] private interactionType type;
    [SerializeField] private int desiredAmount;
    [SerializeField] private int configurationHitCount;

    private int hitCount;
    private UI_Updater uiScript;
    private InteractionConfiguration InteractionConfiguration;
    private PlayerInventory playerInventory;
    private Animator animator;
    private ItemType interactDependeny;
    private bool isInteractDependeny;

    private void Start()
    {
        InteractionConfiguration = InteractionConfiguration.instance;
        animator = this.GetComponent<Animator>();
        uiScript = GameObject.Find("Canvas").GetComponent<UI_Updater>();
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
        SetInteractDependency();
    }
    public void Action()
    {
        switch (type)
        {
            case interactionType.ChopTree:
                InstantiatePrefab(InteractionConfiguration.woodPrefab);
                break;
            case interactionType.StoneCollect:
                playerInventory.GetInventory().AddItem(new Item(ItemType.Stone, desiredAmount));
                this.transform.position = (new Vector3(0, -100));
                Destroy(this.gameObject, 30);
                break;
            case interactionType.StoneDig:
                InstantiatePrefab(InteractionConfiguration.stonePrefab);
                break;
            case interactionType.LightCamfire:
                InstantiatePrefab(InteractionConfiguration.firePrefab);
                break;
            default:
                break;
        }

    }

    private void SetInteractDependency()
    {
        switch (type)
        {
            case interactionType.ChopTree:
                interactDependeny = ItemType.Axe;
                isInteractDependeny = true;
                break;
            case interactionType.StoneCollect:
                isInteractDependeny = false;
                break;
            case interactionType.StoneDig:
                break;
            case interactionType.LightCamfire:
                isInteractDependeny = false;
                break;
            default:
                break;
        }
    }

    public Item GetToolIfAvailable(PlayerInventory playerInventory)
    {
        if (isInteractDependeny == false)
        {
            return null;
        }

        List<Item> inventoryTools = playerInventory.GetInventory().GetTools();
        foreach (Item item in inventoryTools)
        {
            if(item.type==interactDependeny)
            {
                return item;
            }
        }

        return null;
    }


    public void PlayerTrigger(bool isActive)
    {
        uiScript.InteractionScreenPopup(type, isActive);
    }

    private void InstantiatePrefab(GameObject prefab)
    {
        int xDistance = 5;
        int zDistance = 7;
        float fSpacing = 0.5f;
        Transform parentObjectTransform = this.GetComponentInParent<Transform>();
        GameObject objectPrefab = Instantiate(prefab);
        objectPrefab.transform.parent = parentObjectTransform;
        objectPrefab.transform.localPosition = new Vector3(xDistance, 0, zDistance) * fSpacing;
        objectPrefab.transform.rotation = Quaternion.Euler(0, 0, 0);
        objectPrefab.name = type.ToString() + xDistance + "," + zDistance;
        objectPrefab.transform.parent = null;
        objectPrefab.transform.localScale = (new Vector3(1, 1, 1));
        ItemWorld objectPrefabScript = objectPrefab.GetComponent<ItemWorld>();
        objectPrefabScript.amount = desiredAmount;
       
        this.transform.position = (new Vector3(0, -100));
        Destroy(this.gameObject, 30);
    }

    public void Interact()
    {
        switch (type)
        {
            case interactionType.ChopTree:
                hitCount++;
                animator.SetBool("isHit", true);
                CheckForAction(configurationHitCount);
                break;
            case interactionType.StoneCollect:
                Action();
                break;
            case interactionType.StoneDig:
                hitCount++;
                CheckForAction(configurationHitCount);
                break;
            case interactionType.LightCamfire:
                Action();
                break;
            default:
                break;
        }
    }
    private void CheckForAction(int configureCount)
    {
        if (hitCount == configureCount)
        {
            if (animator != null)
            {
                animator.SetBool("isFall", true);
                PlayerTrigger(false);
            }
            else
            { Action();
            }
          
        }
    }

    private void resetAnimatorValue(string valueName)
    {
        animator.SetBool(valueName, false);

    }
    public bool GetInteractDependency()
    {
        return isInteractDependeny;
    }
    public interactionType GetInteractableType()
    {
        return type;
    }
}
