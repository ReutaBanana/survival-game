using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingAssets : MonoBehaviour
{
    public static CraftingAssets instance = null;

    [SerializeField] private Sprite axeCraftingSprite;


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

    public Sprite GetSprite(CraftingRecepieType type)
    {
        switch (type)
        {
            case CraftingRecepieType.AxeRecipe:
                return axeCraftingSprite;
            case CraftingRecepieType.PickaxeRecipe:
                return axeCraftingSprite;
            default:
                return null;
        }
    }
}
