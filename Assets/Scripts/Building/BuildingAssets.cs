using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAssets : MonoBehaviour
{
    public static BuildingAssets instance = null;
    [SerializeField] private GameObject campfireObject;
    [SerializeField] private GameObject campfireObjectWaiting;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    public GameObject getWaitingGameobject(BuildingRecipeType type)
    {
        switch (type)
        {
            case BuildingRecipeType.Campfire:
                return campfireObjectWaiting;
            default:
                return null;
        }
    }

    public GameObject getGameobject(BuildingRecipeType type)
    {
        switch (type)
        {
            case BuildingRecipeType.Campfire:
                return campfireObject;
            default:
                return null;
        }
    }
}
