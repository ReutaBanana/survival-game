using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingRecipeType
{
    Campfire
}
public class BuildingRecipes : MonoBehaviour
{
    public static BuildingRecipes instance = null;

    private List<Recipe> recipes = new List<Recipe>();

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

    public void CreateRecipes()
    {
        recipes.Add(new Recipe(CampfireIngredientsList(),BuildingRecipeType.Campfire, new Item(ItemType.Campfire, 1)));
    }

    private List<Item> CampfireIngredientsList()
    {
        List<Item> axeIngredients = new List<Item>();
        axeIngredients.Add(new Item(ItemType.Wood, 3));
        axeIngredients.Add(new Item(ItemType.Stone, 2));
        return axeIngredients;
    }

    public List<Item> GetRecipie(BuildingRecipeType recipieType)
    {
        switch (recipieType)
        {
            case BuildingRecipeType.Campfire:
                return GetRecipeByType(BuildingRecipeType.Campfire).GetIngredients();
            default:
                return null;
        }
    }
    public List<Recipe> GetAllRecipes()
    {
        return recipes;
    }

    public Item GetBuiltItem(BuildingRecipeType recepieType)
    {
        return GetRecipeByType(recepieType).GetOutputItem();
    }

    private Recipe GetRecipeByType(BuildingRecipeType type)
    {
        foreach (Recipe item in recipes)
        {
            if(item.IsRecipeType(type))
            {
                return item;
            }
        }
        return null;
    }
}
