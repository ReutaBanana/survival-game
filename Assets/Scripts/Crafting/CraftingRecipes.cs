using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RecepieType
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

    public List<Item> GetRecipie(RecepieType recipieType)
    {
        switch (recipieType)
        {
            case RecepieType.AxeRecipe:
                return axeRecepie;
            default:
                return null;
        }
    }

    public Item GetCraftedItem(RecepieType recepieType)
    {
        switch (recepieType)
        {
            case RecepieType.AxeRecipe:
                return new Item(ItemType.Axe, 1);
            default:
                return null;
        }
    }

}
