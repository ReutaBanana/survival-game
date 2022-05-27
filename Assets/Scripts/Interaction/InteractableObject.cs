using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum interactionType
{
    Tree,
    Rock
}


public class InteractableObject : MonoBehaviour
{
    [SerializeField] private interactionType type;
    [SerializeField] private UIUpdater uiScript;

    private int hitCount;
    private InteractionConfiguration InteractionConfiguration;
    private Animator animator;

    private void Start()
    {
        InteractionConfiguration = InteractionConfiguration.instance;
        animator = this.GetComponent<Animator>();
    }
    public void Action()
    {
        switch (type)
        {
            case interactionType.Tree:
                InstantiatePrefab(InteractionConfiguration.woodPrefab);
                break;
            case interactionType.Rock:
                break;
            default:
                break;
        }

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

        this.transform.position = (new Vector3(0, -100));
        Destroy(this.gameObject, 30);
    }

    public void Interact()
    {
        switch (type)
        {
            case interactionType.Tree:
                hitCount++;
                animator.SetBool("isHit", true);
                CheckForAction(InteractionConfiguration.woodHitCount);
                break;
            case interactionType.Rock:
                break;
            default:
                break;
        }
    }
    private void CheckForAction(int configureCount)
    {
        if (hitCount == configureCount)
        {
            animator.SetBool("isFall", true);
            PlayerTrigger(false);
        }
    }

    private void resetAnimatorValue(string valueName)
    {
        animator.SetBool(valueName, false);

    }

}
