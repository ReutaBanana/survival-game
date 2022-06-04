using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CraftingRecepieType
{
    AxeRecipe
}
public class CraftingRecipes: MonoBehaviour 
{
    public static CraftingRecipes instance = null;

   
    private List<Item> axeRecepie = new List<Item>();

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

        CreateRecipes();
    }


    public List<Item> AxeRecipe()
    {
       
        return axeRecepie;
    }

    public void CreateRecipes()
    {
        axeRecepie.Add(new Item(ItemType.Wood, 3));
        axeRecepie.Add(new Item(ItemType.Stone, 2));
    
    }

    public List<Item> GetRecipie(CraftingRecepieType recipieType)
    {
        switch (recipieType)
        {
            case CraftingRecepieType.AxeRecipe:
                return axeRecepie;
            default:
                return null;
        }
    }

    public Item GetCraftedItem(CraftingRecepieType recepieType)
    {
        switch (recepieType)
        {
            case CraftingRecepieType.AxeRecipe:
                return new Item(ItemType.Axe, 1);
            default:
                return null;
        }
    }

}
